using DeltaWare.SDK.MessageBroker.Processors;
using DeltaWare.SDK.MessageBroker.Processors.Results;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Testing.Messages.Processors
{
    public class DirectMessageProcessorA : MessageProcessor<DirectMessage>
    {
        private readonly ILogger _logger;

        public DirectMessageProcessorA(ILogger<DirectMessageProcessorA> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageProcessingResult> ProcessAsync(DirectMessage message)
        {
            _logger.LogInformation($"Direct Received on A! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageProcessingResult.Success());
        }
    }
    public class DirectMessageProcessorB : MessageProcessor<DirectMessage>
    {
        private readonly ILogger _logger;

        public DirectMessageProcessorB(ILogger<DirectMessageProcessorB> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageProcessingResult> ProcessAsync(DirectMessage message)
        {
            _logger.LogInformation($"Direct Received on B! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageProcessingResult.Success());
        }
    }
    public class DirectMessageProcessorC : MessageProcessor<DirectMessage>
    {
        private readonly ILogger _logger;

        public DirectMessageProcessorC(ILogger<DirectMessageProcessorC> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageProcessingResult> ProcessAsync(DirectMessage message)
        {
            _logger.LogInformation($"Direct Received on C! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageProcessingResult.Success());
        }
    }
    public class DirectMessageProcessorD : MessageProcessor<DirectMessage>
    {
        private readonly ILogger _logger;

        public DirectMessageProcessorD(ILogger<DirectMessageProcessorD> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageProcessingResult> ProcessAsync(DirectMessage message)
        {
            _logger.LogInformation($"Direct Received on D! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageProcessingResult.Success());
        }
    }
}
