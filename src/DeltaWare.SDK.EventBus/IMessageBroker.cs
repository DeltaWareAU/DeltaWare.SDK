using DeltaWare.SDK.MessageBroker.Messages;
using DeltaWare.SDK.MessageBroker.Messages.Binding;
using DeltaWare.SDK.MessageBroker.Messages.Consumers;

namespace DeltaWare.SDK.MessageBroker
{
    public interface IMessageBroker
    {
        bool IsEmpty { get; }
        
        void Bind<TMessage, TConsumer>() where TMessage : Message where TConsumer : MessageConsumerBase<TMessage>;
        void Bind<TMessage, TConsumer>(string messageName) where TMessage : Message where TConsumer : MessageConsumerBase<TMessage>;

        bool UnBind<TMessage, TConsumer>() where TMessage : Message where TConsumer : MessageConsumerBase<TMessage>;
        bool UnBind<TConsumer>(string messageName) where TConsumer : IMessageConsumer;

        bool IsMessageBound<TMessage>() where TMessage : Message;
        bool IsMessageBound(string messageName);

        IMessageBinding GetBinding<TMessage>() where TMessage : Message;
        IMessageBinding GetBinding(string messageName);
    }
}
