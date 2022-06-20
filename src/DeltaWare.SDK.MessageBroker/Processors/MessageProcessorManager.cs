using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Serialization;
using DeltaWare.SDK.MessageBroker.Processors.Bindings;
using DeltaWare.SDK.MessageBroker.Processors.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.Processors
{
    public class MessageProcessorManager : IMessageProcessorManager
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IMessageSerializer _messageSerializer;

        public MessageProcessorManager(IServiceScopeFactory serviceScopeFactory, IMessageSerializer messageSerializer)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _messageSerializer = messageSerializer;
        }

        public async Task<IMessageProcessingResult> ProcessMessageAsync(IMessageProcessorBinding processorBinding, string messageData)
        {
            Message message;

            try
            {
                message = _messageSerializer.Deserialize(messageData, processorBinding.MessageType);
            }
            catch (Exception e)
            {
                return MessageProcessingResult.Failure(e, "An exception was encountered whilst deserializing the message");
            }

            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            IMessageProcessor messageProcessor;

            try
            {
                messageProcessor = (IMessageProcessor)scope.ServiceProvider.CreateInstance(processorBinding.ProcessorType);
            }
            catch (Exception e)
            {
                return MessageProcessingResult.Failure(e, $"An exception was encountered whilst instantiating {processorBinding.ProcessorType.Name}");
            }

            return await messageProcessor.ProcessAsync(message);
        }
    }
}
