using DeltaWare.SDK.MessageBroker.Processors.Results;
using System.Threading.Tasks;
using DeltaWare.SDK.MessageBroker.Binding;

namespace DeltaWare.SDK.MessageBroker.Processors
{
    public interface IMessageHandlerManager
    {
        Task<IMessageHandlerResults> HandleMessageAsync(IMessageHandlerBinding handlerBinding, string messageData);
    }
}