using DeltaWare.SDK.Core.Validators;
using DeltaWare.SDK.MessageBroker.Messages.Enums;

namespace DeltaWare.SDK.MessageBroker.Messages.Attributes
{
    public class TopicBindingAttribute : MessageBrokerBindingAttribute
    {
        public TopicBindingAttribute(string name, string routingPattern) : base(name, BrokerExchangeType.Topic, routingPattern)
        {
            StringValidator.ThrowOnNullOrWhitespace(routingPattern, nameof(routingPattern));
        }
    }
}
