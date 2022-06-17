using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.ServiceBus.Broker
{
    internal class ServiceBusMessageBrokerHost : IHostedService
    {
        private readonly IServiceBusMessageBroker _messageBroker;

        private readonly ILogger _logger;

        public ServiceBusMessageBrokerHost(ILogger<ServiceBusMessageBrokerHost> logger, IServiceBusMessageBroker messageBroker)
        {
            _logger = logger;
            _messageBroker = messageBroker;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Message Broker Host.");

            if (!_messageBroker.Initiated)
            {
                _logger.LogDebug("Initiating Message Consumer Bindings");

                _messageBroker.InitiateBindings();
            }

            return _messageBroker.StartListeningAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Message Broker Host.");

            await _messageBroker.StopListeningAsync(cancellationToken);

            while (_messageBroker.IsProcessing)
            {
                _logger.LogInformation("Shutdown postponed until messages have finished being processed.");

                await Task.Delay(1000, cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}
