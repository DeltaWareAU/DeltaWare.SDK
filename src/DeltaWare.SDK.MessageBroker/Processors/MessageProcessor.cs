using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Processors.Results;
using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.Processors
{
    public abstract class MessageProcessor<TMessage> : IMessageProcessor where TMessage : Message
    {
        public async Task<IMessageProcessingResult> ProcessAsync(Message message)
        {
            TMessage messageToProcess;

            try
            {
                messageToProcess = (TMessage)message;
            }
            catch (Exception ex)
            {
                return MessageProcessingResult.Failure(ex, $"Failed to cast incoming message for ({GetType().Name}).");
            }

            try
            {
                return await ProcessAsync(messageToProcess);
            }
            catch (Exception e)
            {
                return MessageProcessingResult.Failure(e);
            }
        }

        protected abstract Task<IMessageProcessingResult> ProcessAsync(TMessage message);
    }
}
