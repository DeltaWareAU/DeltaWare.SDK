using System;
using System.Collections.Generic;
using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Binding;
using DeltaWare.SDK.MessageBroker.Messages.Consumers;
using DeltaWare.SDK.MessageBroker.Settings;

namespace DeltaWare.SDK.MessageBroker
{
    public class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IMessageBrokerSettings _settings;

        private readonly Dictionary<string, MessageBinding> _subscriptions = new();

        public bool IsEmpty => _subscriptions.Count == 0;

        public InMemoryMessageBroker(IMessageBrokerSettings? settings = null)
        {
            _settings = settings ?? new MessageBrokerSettings();
        }

        public void Bind<TMessage, TConsumer>() where TMessage : Message where TConsumer : MessageConsumerBase<TMessage>
        {
            Bind<TMessage, TConsumer>(GetEventNameFromType(typeof(TMessage)));
        }

        public void Bind<TMessage, TConsumer>(string messageName) where TMessage : Message where TConsumer : MessageConsumerBase<TMessage>
        {
            ValidateMessageName(messageName);

            Type consumerType = typeof(TConsumer);

            if (_subscriptions.TryGetValue(messageName, out MessageBinding binding))
            {
                if (binding.Consumers.Contains(consumerType))
                {
                    return;
                }

                binding.Consumers.Add(consumerType);
            }
            else
            {
                binding = new MessageBinding(messageName, typeof(TMessage));
                binding.Consumers.Add(consumerType);

                _subscriptions.Add(messageName, binding);
            }
        }

        public bool UnBind<TMessage, TConsumer>() where TMessage : Message where TConsumer : MessageConsumerBase<TMessage>
        {
            return UnBind<TConsumer>(GetEventNameFromType<TMessage>());
        }

        public bool UnBind<TConsumer>(string messageName) where TConsumer : IMessageConsumer
        {
            ValidateMessageName(messageName);

            if (!_subscriptions.TryGetValue(messageName, out MessageBinding binding))
            {
                return false;
            }

            return binding.Consumers.Remove(typeof(TConsumer));
        }

        public bool IsMessageBound<TMessage>() where TMessage : Message
        {
            return IsMessageBound(GetEventNameFromType<TMessage>());
        }

        public bool IsMessageBound(string messageName)
        {
            ValidateMessageName(messageName);

            return _subscriptions.ContainsKey(messageName);
        }

        public IMessageBinding GetBinding<TMessage>() where TMessage : Message
        {
            return GetBinding(GetEventNameFromType<TMessage>());
        }

        public IMessageBinding GetBinding(string messageName)
        {
            ValidateMessageName(messageName);

            return _subscriptions[messageName];
        }

        protected string GetEventNameFromType<TMessage>() where TMessage : Message
        {
            return GetEventNameFromType(typeof(TMessage));
        }

        protected virtual string GetEventNameFromType(Type messageType)
        {
            return messageType.Name.Replace(_settings.TrimMessageTypeName, string.Empty);
        }

        private void ValidateMessageName(string messageName)
        {
            if (string.IsNullOrWhiteSpace(messageName))
            {
                throw new ArgumentException("A valid Message Name cannot be null or whitespace.");
            }
        }
    }
}
