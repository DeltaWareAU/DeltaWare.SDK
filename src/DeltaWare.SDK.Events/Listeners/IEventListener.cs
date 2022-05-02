using System;

namespace DeltaWare.SDK.Events.Listeners
{
    public interface IEventListener
    {
        DateTimeOffset EventTime { get; }
    }
}