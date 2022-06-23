using DeltaWare.SDK.MessageBroker.Binding.Enums;

namespace DeltaWare.SDK.MessageBroker.Binding.Attributes
{
    public class TopicBindingAttribute : MessageBrokerBindingAttribute
    {
        public TopicBindingAttribute(string name, string? routingPattern = null) : base(name, BrokerExchangeType.Topic, routingPattern)
        {
        }
    }
}
