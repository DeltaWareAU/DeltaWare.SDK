using DeltaWare.SDK.EventBus.Events;

namespace DeltaWare.SDK.EventBus.Settings
{
    public interface IEventBusSettings
    {
        /// <summary>
        /// Specifies the value to be trimmed from the <see cref="IntegrationEvent"/>s name.
        /// </summary>
        /// <remarks>The <see langword="default"/> value is IntegrationEvent.</remarks>
        public string TrimEventTypeName { get; }
    }
}
