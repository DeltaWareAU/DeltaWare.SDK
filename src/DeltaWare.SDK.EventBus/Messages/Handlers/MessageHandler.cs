using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DeltaWare.SDK.MessageBroker.Messages.Binding;
using DeltaWare.SDK.MessageBroker.Messages.Consumers;
using DeltaWare.SDK.MessageBroker.Messages.Processor;

namespace DeltaWare.SDK.MessageBroker.Messages.Handlers
{
    internal class MessageHandler : IMessageHandler
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IMessageBroker _subscriptionManager;

        public MessageHandler(IServiceProvider serviceProvider, IMessageBroker subscriptionManager)
        {
            _serviceProvider = serviceProvider;
            _subscriptionManager = subscriptionManager;
        }

        public Task HandleMessageAsync(string messageName, string messageData)
        {
            IMessageBinding binding = _subscriptionManager.GetBinding(messageName);

            Message message = DeserializeMessage(messageData, binding.MessageType);

            return ExecuteConsumersAsync(GetConsumers(binding), message);
        }

        private Task ExecuteConsumersAsync(IMessageConsumer[] consumers, Message message)
        {
            Task[] consumerTasks = new Task[consumers.Length];

            for (int i = 0; i < consumers.Length; i++)
            {
                consumerTasks[i] = consumers[i].ExecuteAsync(message);
            }

            return Task.WhenAll(consumerTasks);

        }

        private IMessageConsumer[] GetConsumers(IMessageBinding binding)
        {
            return binding.Consumers
                .Select(consumerType => (IMessageConsumer)_serviceProvider.CreateInstance(consumerType))
                .ToArray();
        }

        private Message DeserializeMessage(string eventData, Type eventType)
        {
            return (Message)JsonSerializer.Deserialize(eventData, eventType)!;
        }
    }
}
