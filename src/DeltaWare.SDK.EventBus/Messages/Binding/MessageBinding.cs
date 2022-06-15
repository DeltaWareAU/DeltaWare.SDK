using System;
using System.Collections.Generic;

namespace DeltaWare.SDK.MessageBroker.Messages.Binding
{
    internal class MessageBinding: IMessageBinding
    {
        public string MessageName { get; }
        public Type MessageType { get; }
        public IList<Type> Consumers { get; set; } = new List<Type>();

        public MessageBinding(string messageName, Type messageType)
        {
            if (string.IsNullOrWhiteSpace(messageName))
            {
                throw new ArgumentNullException(nameof(messageName), "A message name must be specified.");
            }

            MessageName = messageName;
            MessageType = messageType;
        }

    }
}
