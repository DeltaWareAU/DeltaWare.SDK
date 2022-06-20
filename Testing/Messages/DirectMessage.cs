using DeltaWare.SDK.MessageBroker.Attributes;
using DeltaWare.SDK.MessageBroker.Messages;

namespace Testing.Messages
{
    [DirectBinding("test.message")]
    public record DirectMessage : Message
    {
        public string TestString { get; set; }

        public int TestInt { get; set; }
    }
}
