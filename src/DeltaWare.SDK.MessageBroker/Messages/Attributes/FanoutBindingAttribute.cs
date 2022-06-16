using DeltaWare.SDK.MessageBroker.Messages.Enums;

namespace DeltaWare.SDK.MessageBroker.Messages.Attributes
{
    public class FanoutBindingAttribute : MessageBrokerBindingAttributeBase
    {
        public FanoutBindingAttribute(string name) : base(name, BrokerExchangeType.Fanout)
        {
        }
    }
}
