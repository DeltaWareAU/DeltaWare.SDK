using DeltaWare.SDK.MessageBroker.Processors;
using DeltaWare.SDK.MessageBroker.Processors.Results;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Testing.Messages.Processors
{
    public class TopicMessageProcessorA : MessageProcessor<TopicMessage>
    {
        private readonly ILogger _logger;

        public TopicMessageProcessorA(ILogger<TopicMessageProcessorA> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageProcessingResult> ProcessAsync(TopicMessage message)
        {
            _logger.LogInformation($"Topic Received on A! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageProcessingResult.Success());
        }
    }

    public class TopicMessageProcessorB : MessageProcessor<TopicMessage>
    {
        private readonly ILogger _logger;

        public TopicMessageProcessorB(ILogger<TopicMessageProcessorB> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageProcessingResult> ProcessAsync(TopicMessage message)
        {
            _logger.LogInformation($"Topic Received on B! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageProcessingResult.Success());
        }
    }
}
