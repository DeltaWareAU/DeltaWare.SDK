using System;

namespace DeltaWare.SDK.Core.Events.Listeners
{
    public interface IEventListener
    {
        DateTimeOffset EventTime { get; }
    }
}