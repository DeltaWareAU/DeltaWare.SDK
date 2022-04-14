using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.SDK.EventBus.Events.Processor
{
    public interface IIntegrationEventProcessor
    {
        Task ProcessEventAsync(string eventName, string eventData);
    }
}
