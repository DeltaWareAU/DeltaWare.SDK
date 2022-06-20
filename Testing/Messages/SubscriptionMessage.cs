using DeltaWare.SDK.MessageBroker.Attributes;
using DeltaWare.SDK.MessageBroker.Messages;

namespace Testing.Messages
{
    [TopicBinding("test.topic")]
    public record SubscriptionMessage : Message
    {
        public string TestString { get; set; }

        public int TestInt { get; set; }
    }
}
