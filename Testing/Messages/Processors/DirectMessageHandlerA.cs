using DeltaWare.SDK.MessageBroker.Processors;
using DeltaWare.SDK.MessageBroker.Processors.Results;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Testing.Messages.Processors
{
    public class DirectMessageHandlerA : MessageHandler<DirectMessage>
    {
        private readonly ILogger _logger;

        public DirectMessageHandlerA(ILogger<DirectMessageHandlerA> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageHandlerResult> ProcessAsync(DirectMessage message)
        {
            _logger.LogInformation($"Direct Received on A! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageHandlerResult.Success());
        }
    }
    public class DirectMessageHandlerB : MessageHandler<DirectMessage>
    {
        private readonly ILogger _logger;

        public DirectMessageHandlerB(ILogger<DirectMessageHandlerB> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageHandlerResult> ProcessAsync(DirectMessage message)
        {
            _logger.LogInformation($"Direct Received on B! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageHandlerResult.Success());
        }
    }
    public class DirectMessageHandlerC : MessageHandler<DirectMessage>
    {
        private readonly ILogger _logger;

        public DirectMessageHandlerC(ILogger<DirectMessageHandlerC> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageHandlerResult> ProcessAsync(DirectMessage message)
        {
            _logger.LogInformation($"Direct Received on C! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageHandlerResult.Success());
        }
    }
    public class DirectMessageHandlerD : MessageHandler<DirectMessage>
    {
        private readonly ILogger _logger;

        public DirectMessageHandlerD(ILogger<DirectMessageHandlerD> logger)
        {
            _logger = logger;
        }

        protected override Task<IMessageHandlerResult> ProcessAsync(DirectMessage message)
        {
            _logger.LogInformation($"Direct Received on D! {message.Id} - String: {message.TestString} - Int: {message.TestInt}");

            return Task.FromResult(MessageHandlerResult.Success());
        }
    }
}
