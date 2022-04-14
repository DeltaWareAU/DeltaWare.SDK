using DeltaWare.SDK.EventBus.Events;

namespace DeltaWare.SDK.EventBus.Settings
{
    internal class EventBusSettings : IEventBusSettings
    {
        public string TrimEventTypeName { get; set; } = nameof(IntegrationEvent);
    }
}
