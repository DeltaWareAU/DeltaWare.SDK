using DeltaWare.SDK.MessageBroker.Processors;
using DeltaWare.SDK.MessageBroker.Processors.Results;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DeltaWare.SDK.MessageBroker.Binding.Attributes;

namespace Testing.Messages.Processors
{
    [RoutingPattern("topic.alpha")]
    public class AlphaSubscriptionMessageConsumer : MessageHandler<SubscriptionMessage>
    {
        private readonly ILogger _logger;

        public AlphaSubscriptionMessageConsumer(ILogger<AlphaSubscriptionMessageConsumer> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageHandlerResult> ProcessAsync(SubscriptionMessage message)
        {
            _logger.LogInformation($"Direct Received on Subscription! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageHandlerResult.Success());
        }
    }

    [RoutingPattern("topic.beta")]
    public class BetaSubscriptionMessageConsumer : MessageHandler<SubscriptionMessage>
    {
        private readonly ILogger _logger;

        public BetaSubscriptionMessageConsumer(ILogger<BetaSubscriptionMessageConsumer> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageHandlerResult> ProcessAsync(SubscriptionMessage message)
        {
            _logger.LogInformation($"Direct Received on Subscription! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageHandlerResult.Success());
        }
    }

    [RoutingPattern("topic.charlie")]
    public class CharlieSubscriptionMessageConsumer : MessageHandler<SubscriptionMessage>
    {
        private readonly ILogger _logger;

        public CharlieSubscriptionMessageConsumer(ILogger<CharlieSubscriptionMessageConsumer> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageHandlerResult> ProcessAsync(SubscriptionMessage message)
        {
            _logger.LogInformation($"Direct Received on Subscription! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageHandlerResult.Success());
        }
    }
}
