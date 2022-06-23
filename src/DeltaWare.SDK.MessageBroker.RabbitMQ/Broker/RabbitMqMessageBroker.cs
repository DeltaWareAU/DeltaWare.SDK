using DeltaWare.SDK.MessageBroker.Binding;
using DeltaWare.SDK.MessageBroker.Binding.Enums;
using DeltaWare.SDK.MessageBroker.Broker;
using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Serialization;
using DeltaWare.SDK.MessageBroker.Processors;
using DeltaWare.SDK.MessageBroker.RabbitMQ.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker.RabbitMQ.Broker
{
    internal class RabbitMqMessageBroker : IMessageBroker, IDisposable
    {
        private readonly IRabbitMqMessageBrokerOptions _options;

        private readonly IConnection _connection;
        
        private readonly IBindingDirector _bindingDirector;

        private readonly IMessageHandlerManager _messageHandlerManager;

        private readonly IMessageSerializer _messageSerializer;

        private IModel _channel;

        private IReadOnlyDictionary<IMessageHandlerBinding, IBasicConsumer> _handlerBindings;

        public bool Initiated { get; private set; }
        public bool IsListening { get; private set; }
        public bool IsProcessing { get; private set; }

        public RabbitMqMessageBroker(IRabbitMqMessageBrokerOptions options, IBindingDirector bindingDirector, IMessageHandlerManager messageHandlerManager, IMessageSerializer messageSerializer)
        {
            _options = options;
            _bindingDirector = bindingDirector;
            _messageHandlerManager = messageHandlerManager;
            _messageSerializer = messageSerializer;
            _connection = OpenConnection(options);
        }

        public Task StopListeningAsync(CancellationToken cancellationToken = default)
        {
            if (!IsListening)
            {
                return Task.CompletedTask;
            }

            IsListening = false;

            _channel.Close();
            _channel.Dispose();
            _channel = null;

            return Task.CompletedTask;
        }

        public Task PublishAsync<TMessage>(TMessage message) where TMessage : Message
        {
            var binding = _bindingDirector.GetMessageBinding<TMessage>();

            string serializedMessage = _messageSerializer.Serialize(message);

            var properties = _channel.CreateBasicProperties();

            byte[] messageBuffer = Encoding.UTF8.GetBytes(serializedMessage);

            _channel.BasicPublish(binding.Name, binding.RoutingPattern ?? string.Empty, properties, messageBuffer);

            return Task.CompletedTask;
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

            Initiated = true;

            Dictionary<IMessageHandlerBinding, IBasicConsumer> handlerBindings = new Dictionary<IMessageHandlerBinding, IBasicConsumer>();

            foreach (IMessageHandlerBinding binding in _bindingDirector.GetHandlerBindings())
            {
                HandlerBindingConsumer consumer = new HandlerBindingConsumer(_messageHandlerManager, binding);

                handlerBindings.Add(binding, consumer);
            }

            _handlerBindings = handlerBindings;
        }

        public Task StartListeningAsync(CancellationToken cancellationToken = default)
        {
            if (IsListening)
            {
                return Task.CompletedTask;
            }

            IsListening = true;

            _channel = _connection.CreateModel();
            
            foreach (KeyValuePair<IMessageHandlerBinding, IBasicConsumer> handlerBinding in _handlerBindings)
            {
                switch (handlerBinding.Key.Details.ExchangeType)
                {
                    case BrokerExchangeType.Fanout:
                    case BrokerExchangeType.Direct:
                        _channel.BasicConsume(handlerBinding.Key.Details.Name, false, handlerBinding.Value);
                        break;
                    case BrokerExchangeType.Topic:
                        _channel.BasicConsume($"{handlerBinding.Key.Details.Name}.{handlerBinding.Key.Details.RoutingPattern}", false, handlerBinding.Value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return Task.CompletedTask;
        }
    }
}
