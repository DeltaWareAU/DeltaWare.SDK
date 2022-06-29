using DeltaWare.SDK.MessageBroker.Binding.Attributes;
using DeltaWare.SDK.MessageBroker.Messages;

namespace Testing.Messages
{
    [TopicBinding("test.topic", "topic.alpha")]
    public class TopicMessage : Message
    {
        public string TestString { get; set; }

        public int TestInt { get; set; }
    }
}
