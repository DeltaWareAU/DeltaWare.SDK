using DeltaWare.SDK.Events.Listeners;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Events
{
    public interface IEventDispatcher
    {
        void Publish<TEvent>(TEvent sender) where TEvent : IEventListener;

        Task PublishAsync<TEvent>(TEvent sender) where TEvent : IEventListener;
    }
}