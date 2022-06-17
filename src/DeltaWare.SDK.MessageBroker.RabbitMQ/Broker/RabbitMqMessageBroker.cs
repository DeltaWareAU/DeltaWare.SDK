using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.RabbitMQ.Options;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.RabbitMQ.Broker
{
    internal class RabbitMqMessageBroker : IRabbitMqMessageBroker, IDisposable
    {
        private readonly IRabbitMqMessageBrokerOptions _options;

        private readonly IConnection _connection;

        public RabbitMqMessageBroker(IRabbitMqMessageBrokerOptions options)
        {
            _options = options;
            _connection = OpenConnection(options);
        }

        public Task SendAsync<TMessage>(TMessage message) where TMessage : Message
        {
            IModel channel = _connection.CreateModel();



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
    }
}
