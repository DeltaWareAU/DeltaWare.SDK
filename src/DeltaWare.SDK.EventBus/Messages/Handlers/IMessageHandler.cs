using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.Messages.Processor
{
    public interface IMessageHandler
    {
        Task HandleMessageAsync(string messageName, string messageData);
    }
}
