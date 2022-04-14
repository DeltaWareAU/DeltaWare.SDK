using DeltaWare.SDK.EventBus.Events;
using DeltaWare.SDK.EventBus.Events.Handlers;
using DeltaWare.SDK.EventBus.Events.Subscriptions;

namespace DeltaWare.SDK.EventBus
{
    public interface IEventBusSubscriptionManager
    {
        bool IsEmpty { get; }
        
        void Subscribe<TEvent, THandler>() where TEvent : IntegrationEvent where THandler : IntegrationEventHandlerBase<TEvent>;
        void Subscribe<TEvent, THandler>(string eventName) where TEvent : IntegrationEvent where THandler : IntegrationEventHandlerBase<TEvent>;

        bool UnSubscribe<TEvent, THandler>() where TEvent : IntegrationEvent where THandler : IntegrationEventHandlerBase<TEvent>;
        bool UnSubscribe<THandler>(string eventName) where THandler : IIntegrationEventHandler;

        bool IsSubscribedEvent<TEvent>() where TEvent : IntegrationEvent;
        bool IsSubscribedEvent(string eventName);

        IEventSubscriptionInfo GetEventSubscriptionInfo<TEvent>() where TEvent : IntegrationEvent;
        IEventSubscriptionInfo GetEventSubscriptionInfo(string eventName);
    }
}
