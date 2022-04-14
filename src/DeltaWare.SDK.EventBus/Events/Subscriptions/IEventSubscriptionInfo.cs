namespace DeltaWare.SDK.EventBus.Events.Subscriptions
{
    public interface IEventSubscriptionInfo
    {
        public string EventName { get; }

        public Type EventType { get; }

        public IList<Type> EventHandlers { get; }
    }
}
