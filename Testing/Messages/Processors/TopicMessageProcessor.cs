using DeltaWare.SDK.MessageBroker.Attributes;
using DeltaWare.SDK.MessageBroker.Processors;
using DeltaWare.SDK.MessageBroker.Processors.Results;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Testing.Messages.Processors
{
    [RoutingPattern("topic.alpha")]
    public class AlphaSubscriptionMessageConsumer : MessageProcessor<SubscriptionMessage>
    {
        private readonly ILogger _logger;

        public AlphaSubscriptionMessageConsumer(ILogger<AlphaSubscriptionMessageConsumer> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageProcessingResult> ProcessAsync(SubscriptionMessage message)
        {
            _logger.LogInformation($"Direct Received on Subscription! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageProcessingResult.Success());
        }
    }

    [RoutingPattern("topic.beta")]
    public class BetaSubscriptionMessageConsumer : MessageProcessor<SubscriptionMessage>
    {
        private readonly ILogger _logger;

        public BetaSubscriptionMessageConsumer(ILogger<BetaSubscriptionMessageConsumer> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageProcessingResult> ProcessAsync(SubscriptionMessage message)
        {
            _logger.LogInformation($"Direct Received on Subscription! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageProcessingResult.Success());
        }
    }

    [RoutingPattern("topic.charlie")]
    public class CharlieSubscriptionMessageConsumer : MessageProcessor<SubscriptionMessage>
    {
        private readonly ILogger _logger;

        public CharlieSubscriptionMessageConsumer(ILogger<CharlieSubscriptionMessageConsumer> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageProcessingResult> ProcessAsync(SubscriptionMessage message)
        {
            _logger.LogInformation($"Direct Received on Subscription! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageProcessingResult.Success());
        }
    }
}
