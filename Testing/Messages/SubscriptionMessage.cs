using DeltaWare.SDK.MessageBroker.Binding.Attributes;
using DeltaWare.SDK.MessageBroker.Messages;

namespace Testing.Messages
{
    [FanoutBinding("test.fanout")]
    public class SubscriptionMessage : Message
    {
        public string TestString { get; set; }

        public int TestInt { get; set; }
    }
}
