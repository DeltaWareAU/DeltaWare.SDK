using DeltaWare.SDK.MessageBroker.Processors.Bindings;
using DeltaWare.SDK.MessageBroker.Processors.Results;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker
{
    public interface IMessageBrokerManager
    {
        Task<IMessageProcessingResult> ProcessMessageAsync(IMessageProcessorBinding processorBinding, string messageData);
    }
}