using DeltaWare.SDK.MessageBroker.Messages.Binding;
using System;

namespace DeltaWare.SDK.MessageBroker.Processors.Bindings
{
    internal class MessageProcessorBinding : IMessageProcessorBinding
    {
        public IBindingDetails Details { get; }
        public Type ProcessorType { get; }
        public Type MessageType { get; }

        public MessageProcessorBinding(Type processorType, IBindingDetails bindingDetails, Type messageType)
        {
            ProcessorType = processorType;
            Details = bindingDetails;
            MessageType = messageType;
        }
    }
}
