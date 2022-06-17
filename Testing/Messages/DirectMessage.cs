using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Attributes;

namespace Testing.Messages
{
    [DirectBinding("test.message")]
    public record DirectMessage : Message
    {
        public string TestString { get; set; }

        public int TestInt { get; set; }
    }
}
