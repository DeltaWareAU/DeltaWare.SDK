﻿using DeltaWare.SDK.MessageBroker.Messages;
using System.Threading.Tasks;

namespace DeltaWare.SDK.MessageBroker
{
    public interface IMessagePublisher
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : Message;
    }
}
