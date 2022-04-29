using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.EventBus.Events.Handlers
{
    public abstract class IntegrationEventHandlerBase<TEvent>: IIntegrationEventHandler where TEvent : IntegrationEvent
    {
        public Type EventType { get; } = typeof(TEvent);

        public Task HandleAsync(IntegrationEvent? integrationEvent)
        {
            if (integrationEvent == null)
            {
                throw new ArgumentNullException(nameof(integrationEvent), $"An Event Handler cannot be invoked without an {nameof(IntegrationEvent)}");
            }

            return HandleAsync((TEvent)integrationEvent);
        }

        protected abstract Task HandleAsync(TEvent integrationEvent);
    }
}
