using DeltaWare.SDK.Events.Listeners;
using System;

namespace DeltaWare.SDK.Events
{
    public interface IEventSubscriber
    {
        void Subscribe<TEvent>(Action<TEvent> listener) where TEvent : IEventListener;

        void UnSubscribe<TEvent>(Action<TEvent> listener) where TEvent : IEventListener;
    }
}