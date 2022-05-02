using System;
using System.Collections.Generic;
using DeltaWare.SDK.EventBus.Events;
using DeltaWare.SDK.EventBus.Events.Handlers;
using DeltaWare.SDK.EventBus.Events.Subscriptions;
using DeltaWare.SDK.EventBus.Settings;

namespace DeltaWare.SDK.EventBus
{
    public class InMemoryEventBusSubscriptionManager : IEventBusSubscriptionManager
    {
        private readonly IEventBusSettings _settings;

        private readonly Dictionary<string, EventSubscriptionInfo> _subscriptions = new();

        public bool IsEmpty => _subscriptions.Count == 0;

        public InMemoryEventBusSubscriptionManager(IEventBusSettings? settings = null)
        {
            _settings = settings ?? new EventBusSettings();
        }

        public void Subscribe<TEvent, THandler>() where TEvent : IntegrationEvent where THandler : IntegrationEventHandlerBase<TEvent>
        {
            Subscribe<TEvent, THandler>(GetEventNameFromType(typeof(TEvent)));
        }

        public void Subscribe<TEvent, THandler>(string eventName) where TEvent : IntegrationEvent where THandler : IntegrationEventHandlerBase<TEvent>
        {
            ValidateEventName(eventName);

            Type handlerType = typeof(THandler);

            if (_subscriptions.TryGetValue(eventName, out EventSubscriptionInfo subscriptionInfo))
            {
                if (subscriptionInfo.EventHandlers.Contains(handlerType))
                {
                    return;
                }

                subscriptionInfo.EventHandlers.Add(handlerType);
            }
            else
            {
                subscriptionInfo = new EventSubscriptionInfo(eventName, typeof(TEvent));
                subscriptionInfo.EventHandlers.Add(handlerType);

                _subscriptions.Add(eventName, subscriptionInfo);
            }
        }

        public bool UnSubscribe<TEvent, THandler>() where TEvent : IntegrationEvent where THandler : IntegrationEventHandlerBase<TEvent>
        {
            return UnSubscribe<THandler>(GetEventNameFromType<TEvent>());
        }

        public bool UnSubscribe<THandler>(string eventName) where THandler : IIntegrationEventHandler
        {
            ValidateEventName(eventName);

            if (!_subscriptions.TryGetValue(eventName, out EventSubscriptionInfo subscriptionInfo))
            {
                return false;
            }

            return subscriptionInfo.EventHandlers.Remove(typeof(THandler));
        }

        public bool IsSubscribedEvent<TEvent>() where TEvent : IntegrationEvent
        {
            return IsSubscribedEvent(GetEventNameFromType<TEvent>());
        }

        public bool IsSubscribedEvent(string eventName)
        {
            ValidateEventName(eventName);

            return _subscriptions.ContainsKey(eventName);
        }

        public IEventSubscriptionInfo GetEventSubscriptionInfo<TEvent>() where TEvent : IntegrationEvent
        {
            return GetEventSubscriptionInfo(GetEventNameFromType<TEvent>());
        }

        public IEventSubscriptionInfo GetEventSubscriptionInfo(string eventName)
        {
            ValidateEventName(eventName);

            return _subscriptions[eventName];
        }

        protected string GetEventNameFromType<TEvent>() where TEvent : IntegrationEvent
        {
            return GetEventNameFromType(typeof(TEvent));
        }

        protected virtual string GetEventNameFromType(Type eventType)
        {
            return eventType.Name.Replace(_settings.TrimEventTypeName, string.Empty);
        }

        private void ValidateEventName(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException("A valid Event Name cannot be null or whitespace.");
            }
        }
    }
}
