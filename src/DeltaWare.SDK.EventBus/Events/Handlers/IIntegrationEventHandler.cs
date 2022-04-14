namespace DeltaWare.SDK.EventBus.Events.Handlers
{
    public interface IIntegrationEventHandler
    {
        Type EventType { get; }

        Task HandleAsync(IntegrationEvent? integrationEvent);
    }
}
