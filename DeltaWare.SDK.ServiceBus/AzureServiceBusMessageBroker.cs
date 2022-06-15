using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using DeltaWare.SDK.MessageBroker;

namespace DeltaWare.SDK.ServiceBus
{
    public class AzureServiceBusMessageBroker
    {
        private readonly IMessageBroker _subscriptionManager;

        private readonly IServiceCollection _serviceCollection;

        public AzureServiceBusMessageBroker(IServiceCollection serviceCollection)
        {
        }

        private Task RegisterServiceBusEventHandler()
        {
            _processor.ProcessMessageAsync += HandleMessageAsync;
            _processor.ProcessErrorAsync += HandlerErrorAsync;

            return _processor.StartProcessingAsync();
        }

        protected virtual Task HandlerErrorAsync(ProcessErrorEventArgs arg)
        {
            throw new NotImplementedException();
        }

        private async Task HandleMessageAsync(ProcessMessageEventArgs arg)
        {
        }

        protected virtual Task HandleEventAsync(Type eventType, string message)
        {

        }
    }
}
