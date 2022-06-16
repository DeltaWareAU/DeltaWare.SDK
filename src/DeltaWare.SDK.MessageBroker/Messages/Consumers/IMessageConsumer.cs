using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.Messages.Consumers
{
    public interface IMessageConsumer
    {
        Task ExecuteAsync(Message message);
    }
}
