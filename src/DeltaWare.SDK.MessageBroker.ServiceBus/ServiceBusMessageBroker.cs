using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Serialization;

namespace DeltaWare.SDK.MessageBroker.ServiceBus
{
    internal class ServiceBusMessageBroker : IMessageBroker
    {
        private readonly ServiceBusClient _serviceBusClient;

        private readonly IMessageBrokerManager _messageBrokerManager;

        private readonly IMessageSerializer _messageSerializer;

        public ServiceBusMessageBroker(ServiceBusClient serviceBusClient, IMessageBrokerManager messageBrokerManager, IMessageSerializer messageSerializer)
        {
            _serviceBusClient = serviceBusClient;
            _messageBrokerManager = messageBrokerManager;
            _messageSerializer = messageSerializer;
        }

        public async Task SendAsync<TMessage>(TMessage message) where TMessage : Message
        {
            IBindingDetails bindingDetails = _messageBrokerManager.GetMessageBinding<TMessage>();

            await using ServiceBusSender sender = _serviceBusClient.CreateSender(bindingDetails.Name);

            ServiceBusMessage serviceBusMessage = CreateServiceBusMessage(message);

            await sender.SendMessageAsync(serviceBusMessage);
        }

        private ServiceBusMessage CreateServiceBusMessage<TMessage>(TMessage message) where TMessage : Message
        {
            string messageBody = _messageSerializer.Serialize(message);

            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(messageBody)
            {
                CorrelationId = message.Id.ToString()
            };

            return serviceBusMessage;
        }
    }
}
