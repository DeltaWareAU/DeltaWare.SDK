using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Attributes;
using DeltaWare.SDK.MessageBroker.Messages.Binding;
using DeltaWare.SDK.MessageBroker.Processors;
using DeltaWare.SDK.MessageBroker.Processors.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DeltaWare.SDK.MessageBroker
{
    public interface IBindingManager
    {
        IEnumerable<IMessageProcessorBinding> GetBindings();

        IBindingDetails GetMessageBinding<T>() where T : Message;
    }

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

        public IEnumerable<IMessageProcessorBinding> GetBindings() => _messageProcessors.SelectMany(map => map.Value);

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

                MessageProcessorBinding processorBinding = new MessageProcessorBinding(processorType, _messageToBindingMap[messageType], messageType);

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
