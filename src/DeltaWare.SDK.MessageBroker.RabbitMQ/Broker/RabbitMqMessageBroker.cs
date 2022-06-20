using DeltaWare.SDK.MessageBroker.Binding;
using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Binding;
using DeltaWare.SDK.MessageBroker.Messages.Enums;
using DeltaWare.SDK.MessageBroker.Processors.Bindings;
using DeltaWare.SDK.MessageBroker.RabbitMQ.Options;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.RabbitMQ.Broker
{
    internal class RabbitMqMessageBroker : IMessageBroker, IDisposable
    {
        private readonly IRabbitMqMessageBrokerOptions _options;

        private readonly IConnection _connection;

        private readonly IModel _channel;

        private readonly IBindingManager _bindingManager;

        public bool Initiated { get; private set; }
        public bool IsListening { get; private set; }
        public bool IsProcessing { get; private set; }

        public RabbitMqMessageBroker(IRabbitMqMessageBrokerOptions options)
        {
            _options = options;
            _connection = OpenConnection(options);
            _channel = _connection.CreateModel();
        }

        public Task StopListeningAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync<TMessage>(TMessage message) where TMessage : Message
        {
            throw new NotImplementedException();
        }

        private IConnection OpenConnection(IRabbitMqMessageBrokerOptions options)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = options.UserName,
                Password = options.Password,
                VirtualHost = options.VirtualHost,
                HostName = options.HostName,
                Port = options.Port
            };

            return factory.CreateConnection();
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }

        public void InitiateBindings()
        {
            if (Initiated)
            {
                return;
            }

            foreach (IBindingDetails messageBinding in _bindingManager.GetMessageBindings())
            {
                _channel.QueueDeclare(messageBinding.Name, false, false, false, null);
            }

            foreach (IMessageProcessorBinding binding in _bindingManager.GetProcessorBindings())
            {
                switch (binding.Details.ExchangeType)
                {
                    case BrokerExchangeType.Direct:
                        break;
                    case BrokerExchangeType.Topic:
                        break;
                    case BrokerExchangeType.Fanout:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Initiated = true;
        }

        public Task StartListeningAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
