using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.Messages.Consumers
{
    public abstract class MessageConsumerBase<TMessage>: IMessageConsumer where TMessage : Message
    {
        public Type MessageType { get; } = typeof(TMessage);

        public Task ExecuteAsync(Message? message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), $"A Message Handler cannot be invoked without an {nameof(Message)}");
            }

            return ExecuteAsync((TMessage)message);
        }

        protected abstract Task ExecuteAsync(TMessage integrationEvent);
    }
}
