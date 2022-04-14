using System.Globalization;

namespace DeltaWare.SDK.EventBus.Events.Subscriptions
{
    internal class EventSubscriptionInfo: IEventSubscriptionInfo
    {
        public string EventName { get; }
        public Type EventType { get; }
        public IList<Type> EventHandlers { get; set; } = new List<Type>();

        public EventSubscriptionInfo(string eventName, Type eventType)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentNullException(nameof(eventName), "An event name must be specified.");
            }

            EventName = eventName;
            EventType = eventType;
        }

    }
}
