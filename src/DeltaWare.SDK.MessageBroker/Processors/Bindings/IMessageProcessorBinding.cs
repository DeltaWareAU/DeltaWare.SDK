using DeltaWare.SDK.MessageBroker.Messages.Binding;
using System;

namespace DeltaWare.SDK.MessageBroker.Processors.Bindings
{
    public interface IMessageProcessorBinding
    {
        IBindingDetails Details { get; }

        Type ProcessorType { get; }
        Type MessageType { get; }
    }
}
