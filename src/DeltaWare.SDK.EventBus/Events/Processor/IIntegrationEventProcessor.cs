using System.Threading.Tasks;

namespace DeltaWare.SDK.EventBus.Events.Processor
{
    public interface IIntegrationEventProcessor
    {
        Task ProcessEventAsync(string eventName, string eventData);
    }
}
