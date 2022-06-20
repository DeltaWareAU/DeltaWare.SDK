using DeltaWare.SDK.MessageBroker.Messages.Enums;

namespace DeltaWare.SDK.MessageBroker.Attributes
{
    public class TopicBindingAttribute : MessageBrokerBindingAttribute
    {
        public TopicBindingAttribute(string name, string? routingPattern = null) : base(name, BrokerExchangeType.Topic, routingPattern)
        {
        }
    }
}
