using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Attributes;
using DeltaWare.SDK.MessageBroker.Messages.Consumers;
using DeltaWare.SDK.MessageBroker.Messages.Serialization;

namespace DeltaWare.SDK.MessageBroker
{
    public interface IMessageBrokerManager
    {
        IEnumerable<IBindingDetails> GetBindings();

        IBindingDetails GetMessageBinding<T>() where T : Message;

        Task ProcessMessageAsync(IBindingDetails bindingDetails, string messageData);
    }

    public class MessageBrokerManager : IMessageBrokerManager
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IMessageSerializer _messageSerializer;

        private readonly Dictionary<IBindingDetails, Type> _bindingToTypeMap = new();

        private readonly Dictionary<Type, IBindingDetails> _typeToBindingMap = new();

        private readonly Dictionary<Type, HashSet<Type>> _boundConsumers = new();

        public MessageBrokerManager(IServiceProvider serviceProvider, IMessageSerializer messageSerializer)
        {
            _serviceProvider = serviceProvider;
            _messageSerializer = messageSerializer;

            DiscoverMessagesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            DiscoverConsumersFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }

        public IEnumerable<IBindingDetails> GetBindings() => _bindingToTypeMap.Keys;

        public IBindingDetails GetMessageBinding<T>() where T : Message => _typeToBindingMap[typeof(T)];

        public async Task ProcessMessageAsync(IBindingDetails bindingDetails, string messageData)
        {
            Type? messageType = _bindingToTypeMap[bindingDetails];

            if (messageType == null)
            {
                throw new Exception();
            }

            foreach (Type consumerType in _boundConsumers[messageType])
            {
                IMessageConsumer messageConsumer = (IMessageConsumer) _serviceProvider.CreateInstance(consumerType);

                Message message = _messageSerializer.Deserialize(messageData, messageType);

                await messageConsumer.ExecuteAsync(message);
            }
        }

        private void DiscoverConsumersFromAssemblies(params Assembly[] assemblies)
        {
            var consumerTypes = GetConsumerTypesFromAssemblies(assemblies);

            foreach (Type consumerType in consumerTypes)
            {
                Type? messageType = consumerType.GetGenericArguments(typeof(MessageConsumerBase<>)).FirstOrDefault();

                if (messageType == null)
                {
                    throw new Exception();
                }

                if (!messageType.IsSubclassOf<Message>())
                {
                    throw new Exception();
                }

                if (_boundConsumers.TryGetValue(messageType, out HashSet<Type> boundConsumerTypes))
                {
                    boundConsumerTypes.Add(consumerType);
                }
                else
                {
                    _boundConsumers.Add(messageType, new HashSet<Type>{ consumerType });
                }
            }
        }

        private void DiscoverMessagesFromAssemblies(params Assembly[] assemblies)
        {
            foreach (Type messageType in GetMessageTypesFromAssemblies(assemblies))
            {
                var bindingAttribute = messageType.GetCustomAttribute<MessageBrokerBindingAttributeBase>();

                if (bindingAttribute == null)
                {
                    throw new Exception($"A message ({messageType.Name}) does not have a Binding Attribute Applied.");
                }

                IBindingDetails bindingDetails = bindingAttribute.GetBindingDetails();
                
                _bindingToTypeMap.Add(bindingDetails, messageType);
                _typeToBindingMap.Add(messageType, bindingDetails);
            }
        }

        private IEnumerable<Type> GetConsumerTypesFromAssemblies(params Assembly[] assemblies)
        {
            return assemblies.SelectMany(a => a.GetLoadedTypes().Where(t => t.IsSubclassOfRawGeneric(typeof(MessageConsumerBase<>))));

        }

        private IEnumerable<Type> GetMessageTypesFromAssemblies(params Assembly[] assemblies)
        {
            return assemblies.SelectMany(a => a.GetLoadedTypes().Where(t => t.IsSubclassOf<Message>()));

        }
    }
}