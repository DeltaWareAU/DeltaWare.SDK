using DeltaWare.SDK.Core.Events.Listeners;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Core.Events
{
    public interface IEventDispatcher
    {
        void Publish<TEvent>(TEvent sender) where TEvent : IEventListener;

        Task PublishAsync<TEvent>(TEvent sender) where TEvent : IEventListener;
    }
}