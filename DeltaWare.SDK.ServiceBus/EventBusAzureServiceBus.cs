using Azure.Messaging.ServiceBus;
using DeltaWare.SDK.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.ServiceBus
{
    public class EventBusAzureServiceBus
    {
        private readonly IEventBusSubscriptionManager _subscriptionManager;

        private readonly IServiceCollection _serviceCollection;

        public EventBusAzureServiceBus(IServiceCollection serviceCollection)
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
