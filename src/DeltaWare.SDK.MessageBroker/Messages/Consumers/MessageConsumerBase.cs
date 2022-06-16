using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.Messages.Consumers
{
    public abstract class MessageConsumerBase<TMessage> : IMessageConsumer where TMessage : Message
    {
        public Task ExecuteAsync(Message message)
        {
            return ExecuteAsync((TMessage)message);
        }

        protected abstract Task ExecuteAsync(TMessage message);
    }
}
