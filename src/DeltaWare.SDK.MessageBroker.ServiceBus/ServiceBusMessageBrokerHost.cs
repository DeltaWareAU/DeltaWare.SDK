using Azure.Messaging.ServiceBus;
using DeltaWare.SDK.MessageBroker.Messages.Enums;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.ServiceBus
{
    internal class ServiceBusMessageBrokerHost : IHostedService, IAsyncDisposable
    {
        private readonly ServiceBusClient _serviceBusClient;

        private readonly IMessageBrokerManager _messageBrokerManager;

        private readonly IReadOnlyDictionary<IBindingDetails, ServiceBusProcessor> _processorBindings;

        private readonly ILogger _logger;

        public ServiceBusMessageBrokerHost(ILogger<ServiceBusMessageBrokerHost> logger, ServiceBusClient serviceBusClient, IMessageBrokerManager messageBrokerManager)
        {
            _logger = logger;

            _serviceBusClient = serviceBusClient;

            _messageBrokerManager = messageBrokerManager;

            _processorBindings = InitiateBindings();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Task> tasks = _processorBindings.Values.Select(p => p.StartProcessingAsync(cancellationToken));

            return Task.WhenAll(tasks);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Task> tasks = _processorBindings.Values.Select(p => p.StopProcessingAsync(cancellationToken));

            return Task.WhenAll(tasks);
        }

        private IReadOnlyDictionary<IBindingDetails, ServiceBusProcessor> InitiateBindings()
        {
            Dictionary<IBindingDetails, ServiceBusProcessor> processorBindings = new Dictionary<IBindingDetails, ServiceBusProcessor>();

            foreach (IBindingDetails binding in _messageBrokerManager.GetBindings())
            {
                ServiceBusProcessor processor;

                if (binding.ExchangeType == BrokerExchangeType.Direct)
                {
                    processor = _serviceBusClient.CreateProcessor(binding.Name);
                }
                else
                {
                    processor = _serviceBusClient.CreateProcessor(binding.Name, binding.RoutingPattern ?? string.Empty);
                }

                processor.ProcessMessageAsync += args => OnMessageAsync(args, binding);
                processor.ProcessErrorAsync += args => OnErrorsAsync(args, binding);

                processorBindings.Add(binding, processor);
            }

            return processorBindings;
        }

        public async Task OnMessageAsync(ProcessMessageEventArgs args, IBindingDetails binding)
        {
            try
            {
                await _messageBrokerManager.ProcessMessageAsync(binding, args.Message.Body.ToString());

                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception e)
            {
                await args.DeadLetterMessageAsync(args.Message, e.Message);
            }
        }

        public Task OnErrorsAsync(ProcessErrorEventArgs args, IBindingDetails binding)
        {
            _logger.LogError(args.Exception, $"Failed to process {binding.Name}");

            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            IEnumerable<Task> tasks = _processorBindings.Values.Select(v => v.DisposeAsync().AsTask());

            await Task.WhenAll(tasks);
        }
    }
}
