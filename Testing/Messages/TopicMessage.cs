using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Attributes;

namespace Testing.Messages
{
    [TopicBinding("test.topic", "topic.alpha")]
    public record TopicMessage : Message
    {
        public string TestString { get; set; }

        public int TestInt { get; set; }
    }
}
