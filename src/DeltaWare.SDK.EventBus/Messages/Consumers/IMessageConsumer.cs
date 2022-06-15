using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.Messages.Consumers
{
    public interface IMessageConsumer
    {
        Type MessageType { get; }

        Task ExecuteAsync(Message? message);
    }
}
