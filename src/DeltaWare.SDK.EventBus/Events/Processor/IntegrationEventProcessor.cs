using DeltaWare.SDK.EventBus.Events.Handlers;
using DeltaWare.SDK.EventBus.Events.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeltaWare.SDK.EventBus.Events.Processor
{
    internal class IntegrationEventProcessor : IIntegrationEventProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IEventBusSubscriptionManager _subscriptionManager;

        public IntegrationEventProcessor(IServiceProvider serviceProvider, IEventBusSubscriptionManager subscriptionManager)
        {
            _serviceProvider = serviceProvider;
            _subscriptionManager = subscriptionManager;
        }

        public async Task ProcessEventAsync(string eventName, string eventData)
        {
            IEventSubscriptionInfo subscriptionInfo = _subscriptionManager.GetEventSubscriptionInfo(eventName);

            IntegrationEvent integrationEvent = DeserializeEventDate(eventData, subscriptionInfo.EventType);

            foreach (IIntegrationEventHandler handler in GetHandlerInstances(subscriptionInfo))
            {
                await handler.HandleAsync(integrationEvent);
            }
        }

        private IEnumerable<IIntegrationEventHandler> GetHandlerInstances(IEventSubscriptionInfo subscriptionInfo)
        {
            return subscriptionInfo.EventHandlers.Select(handler => (IIntegrationEventHandler)_serviceProvider.CreateInstance(handler));
        }

        private IntegrationEvent DeserializeEventDate(string eventData, Type eventType)
        {
            return (IntegrationEvent)JsonSerializer.Deserialize(eventData, eventType)!;
        }
    }
}
