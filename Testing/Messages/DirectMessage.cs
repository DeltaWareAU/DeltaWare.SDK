using DeltaWare.SDK.MessageBroker.Binding.Attributes;
using DeltaWare.SDK.MessageBroker.Messages;

namespace Testing.Messages
{
    [DirectBinding("test.message")]
    public class DirectMessage : Message
    {
        public string TestString { get; set; }

        public int TestInt { get; set; }
    }
}
