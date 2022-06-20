﻿using DeltaWare.SDK.MessageBroker.Attributes;
using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Binding;
using DeltaWare.SDK.MessageBroker.Messages.Enums;
using DeltaWare.SDK.MessageBroker.Processors;
using DeltaWare.SDK.MessageBroker.Processors.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DeltaWare.SDK.MessageBroker.Binding
{
    public class BindingManager : IBindingManager
    {
        private readonly Dictionary<Type, IBindingDetails> _messageToBindingMap = new();

        private readonly Dictionary<Type, List<IMessageProcessorBinding>> _messageProcessors = new();

        public BindingManager()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            DiscoverMessagesFromAssemblies(assemblies);
            DiscoverProcessorsFromAssemblies(assemblies);
        }

        public IEnumerable<IMessageProcessorBinding> GetProcessorBindings() => _messageProcessors.SelectMany(map => map.Value);
        public IEnumerable<IBindingDetails> GetMessageBindings() => _messageToBindingMap.Select(map => map.Value);

        public IBindingDetails GetMessageBinding<T>() where T : Message => _messageToBindingMap[typeof(T)];

        private void DiscoverProcessorsFromAssemblies(params Assembly[] assemblies)
        {
            foreach (Type processorType in GetProcessorTypesFromAssemblies(assemblies))
            {
                Type? messageType = processorType.GetGenericArguments(typeof(MessageProcessor<>)).FirstOrDefault();

                if (messageType == null)
                {
                    throw new Exception();
                }

                if (!messageType.IsSubclassOf<Message>())
                {
                    throw new Exception();
                }

                IBindingDetails binding = _messageToBindingMap[messageType];

                if (processorType.TryGetCustomAttribute(out RoutingPatternAttribute routingPattern))
                {
                    binding = new BindingDetails
                    {
                        ExchangeType = BrokerExchangeType.Topic,
                        Name = binding.Name,
                        RoutingPattern = routingPattern.Pattern
                    };
                }

                MessageProcessorBinding processorBinding = new MessageProcessorBinding(processorType, binding, messageType);

                if (_messageProcessors.TryGetValue(messageType, out List<IMessageProcessorBinding> processorBindings))
                {
                    processorBindings.Add(processorBinding);
                }
                else
                {
                    _messageProcessors.Add(messageType, new List<IMessageProcessorBinding> { processorBinding });
                }
            }
        }

        private void DiscoverMessagesFromAssemblies(params Assembly[] assemblies)
        {
            foreach (Type messageType in GetMessageTypesFromAssemblies(assemblies))
            {
                var bindingAttribute = messageType.GetCustomAttribute<MessageBrokerBindingAttribute>();

                if (bindingAttribute == null)
                {
                    throw new Exception($"A message ({messageType.Name}) does not have a Binding Attribute Applied.");
                }

                IBindingDetails bindingDetails = bindingAttribute.GetBindingDetails();

                _messageToBindingMap.Add(messageType, bindingDetails);
            }
        }

        private IEnumerable<Type> GetProcessorTypesFromAssemblies(params Assembly[] assemblies)
        {
            return assemblies.SelectMany(a => a.GetLoadedTypes().Where(t => t.IsSubclassOfRawGeneric(typeof(MessageProcessor<>))));

        }

        private IEnumerable<Type> GetMessageTypesFromAssemblies(params Assembly[] assemblies)
        {
            return assemblies.SelectMany(a => a.GetLoadedTypes().Where(t => t.IsSubclassOf<Message>()));

        }
    }
}
