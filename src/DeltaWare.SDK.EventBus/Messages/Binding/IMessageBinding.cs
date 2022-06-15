using System;
using System.Collections.Generic;

namespace DeltaWare.SDK.MessageBroker.Messages.Binding
{
    public interface IMessageBinding
    {
        public string MessageName { get; }

        public Type MessageType { get; }

        public IList<Type> Consumers { get; }
    }
}
