using DeltaWare.SDK.MessageBroker.Messages;

namespace DeltaWare.SDK.MessageBroker.Settings
{
    internal class MessageBrokerSettings : IMessageBrokerSettings
    {
        public string TrimMessageTypeName { get; set; } = nameof(Message);
    }
}
