using DeltaWare.SDK.MessageBroker.Messages.Enums;

namespace DeltaWare.SDK.MessageBroker.Attributes
{
    public class DirectBindingAttribute : MessageBrokerBindingAttribute
    {
        public DirectBindingAttribute(string name) : base(name, BrokerExchangeType.Direct)
        {
        }
    }
}
