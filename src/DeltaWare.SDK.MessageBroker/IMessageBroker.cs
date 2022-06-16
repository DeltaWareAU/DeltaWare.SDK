using DeltaWare.SDK.MessageBroker.Messages;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker
{
    public interface IMessageBroker
    {
        Task SendAsync<TMessage>(TMessage message) where TMessage : Message;
    }
}
