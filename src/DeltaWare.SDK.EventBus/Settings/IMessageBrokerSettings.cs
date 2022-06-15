using DeltaWare.SDK.MessageBroker.Messages;

namespace DeltaWare.SDK.MessageBroker.Settings
{
    public interface IMessageBrokerSettings
    {
        /// <summary>
        /// Specifies the value to be trimmed from the <see cref="Message"/>s name.
        /// </summary>
        /// <remarks>The <see langword="default"/> value is IntegrationEvent.</remarks>
        public string TrimMessageTypeName { get; }
    }
}
