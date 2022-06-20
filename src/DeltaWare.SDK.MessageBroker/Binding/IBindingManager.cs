using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Binding;
using DeltaWare.SDK.MessageBroker.Processors.Bindings;
using System.Collections.Generic;

namespace DeltaWare.SDK.MessageBroker.Binding
{
    public interface IBindingManager
    {
        IEnumerable<IMessageProcessorBinding> GetProcessorBindings();

        IEnumerable<IBindingDetails> GetMessageBindings();

        IBindingDetails GetMessageBinding<T>() where T : Message;
    }
}
