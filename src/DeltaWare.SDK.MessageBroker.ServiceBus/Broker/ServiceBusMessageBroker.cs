using Azure.Messaging.ServiceBus;
using DeltaWare.SDK.MessageBroker.Binding;
using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Binding;
using DeltaWare.SDK.MessageBroker.Messages.Enums;
using DeltaWare.SDK.MessageBroker.Messages.Serialization;
using DeltaWare.SDK.MessageBroker.Processors;
using DeltaWare.SDK.MessageBroker.Processors.Bindings;
using DeltaWare.SDK.MessageBroker.Processors.Results;
using DeltaWare.SDK.MessageBroker.ServiceBus.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.ServiceBus.Broker
{
    internal class ServiceBusMessageBroker : IMessageBroker, IAsyncDisposable
    {
        private readonly ServiceBusClient _serviceBusClient;

        private readonly IBindingManager _bindingManager;

        private readonly IMessageProcessorManager _messageProcessorManager;

        private readonly IMessageSerializer _messageSerializer;

        private readonly Dictionary<IBindingDetails, ServiceBusSender> _boundSenders = new();

        private IReadOnlyDictionary<IMessageProcessorBinding, ServiceBusProcessor> _processorBindings;

        private readonly ILogger _logger;

        public bool Initiated { get; private set; }
        public bool IsListening { get; private set; }
        public bool IsProcessing => _processorBindings?.Values.Any(p => p.IsProcessing) ?? false;

        public ServiceBusMessageBroker(ILogger<ServiceBusMessageBroker> logger, IServiceBusMessageBrokerOptions options, IMessageProcessorManager messageProcessorManager, IMessageSerializer messageSerializer, IBindingManager bindingManager)
        {
            _logger = logger;
            _serviceBusClient = new ServiceBusClient(options.ConnectionString);
            _messageProcessorManager = messageProcessorManager;
            _messageSerializer = messageSerializer;
            _bindingManager = bindingManager;
        }

        public async Task PublishAsync<TMessage>(TMessage message) where TMessage : Message
        {
            IBindingDetails bindingDetails = _bindingManager.GetMessageBinding<TMessage>();

            if (!_boundSenders.TryGetValue(bindingDetails, out ServiceBusSender sender))
            {
                sender = _serviceBusClient.CreateSender(bindingDetails.Name);

                _boundSenders.Add(bindingDetails, sender);
            }

            ServiceBusMessage serviceBusMessage = CreateServiceBusMessage(message);

            await sender.SendMessageAsync(serviceBusMessage);
        }

        private ServiceBusMessage CreateServiceBusMessage<TMessage>(TMessage message) where TMessage : Message
        {
            string messageBody = _messageSerializer.Serialize(message);

            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(messageBody)
            {
                CorrelationId = message.Id.ToString()
            };

            return serviceBusMessage;
        }

        public void InitiateBindings()
        {
            if (Initiated)
            {
                return;
            }

            Dictionary<IMessageProcessorBinding, ServiceBusProcessor> processorBindings = new Dictionary<IMessageProcessorBinding, ServiceBusProcessor>();

            foreach (IMessageProcessorBinding binding in _bindingManager.GetProcessorBindings())
            {
                ServiceBusProcessor processor;

                switch (binding.Details.ExchangeType)
                {
                    case BrokerExchangeType.Direct:
                        processor = _serviceBusClient.CreateProcessor(binding.Details.Name);
                        break;
                    case BrokerExchangeType.Topic:
                    case BrokerExchangeType.Fanout:
                        processor = _serviceBusClient.CreateProcessor(binding.Details.Name, binding.Details.RoutingPattern ?? string.Empty);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                processor.ProcessMessageAsync += args => OnMessageAsync(args, binding);
                processor.ProcessErrorAsync += args => OnErrorsAsync(args, binding);

                processorBindings.Add(binding, processor);
            }

            _processorBindings = processorBindings;

            Initiated = true;
        }

        public async Task StartListeningAsync(CancellationToken cancellationToken)
        {
            if (IsListening)
            {
                return;
            }

            IEnumerable<Task> tasks = _processorBindings.Values.Select(p => p.StartProcessingAsync(cancellationToken));

            IsListening = true;

            await Task.WhenAll(tasks);

            IsListening = true;
        }

        public async Task StopListeningAsync(CancellationToken cancellationToken)
        {
            if (!IsListening)
            {
                return;
            }

            IEnumerable<Task> tasks = _processorBindings.Values.Select(p => p.StopProcessingAsync(cancellationToken));

            await Task.WhenAll(tasks);

            IsListening = false;
        }

        private async Task OnMessageAsync(ProcessMessageEventArgs args, IMessageProcessorBinding binding)
        {
            IMessageProcessingResult result = await _messageProcessorManager.ProcessMessageAsync(binding, args.Message.Body.ToString());

            if (result.WasSuccessful)
            {
                await args.CompleteMessageAsync(args.Message);
            }
            else
            {
                if (!result.Retry)
                {
                    await args.DeadLetterMessageAsync(args.Message, result.Message);
                }
            }
        }

        private Task OnErrorsAsync(ProcessErrorEventArgs args, IMessageProcessorBinding binding)
        {
            _logger.LogError(args.Exception, $"Failed to process {binding.Details.Name}");

            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            foreach (ServiceBusProcessor processor in _processorBindings.Values)
            {
                await processor.DisposeAsync();
            }

            foreach (ServiceBusSender sender in _boundSenders.Values)
            {
                await sender.DisposeAsync();
            }

            await _serviceBusClient.DisposeAsync();
        }
    }
}
