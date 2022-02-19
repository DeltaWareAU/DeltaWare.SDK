using DeltaWare.SDK.Core.Events.Listeners;
using System;

namespace DeltaWare.SDK.Core.Events
{
    public interface IEventSubscriber
    {
        void Subscribe<TEvent>(Action<TEvent> listener) where TEvent : IEventListener;

        void UnSubscribe<TEvent>(Action<TEvent> listener) where TEvent : IEventListener;
    }
}