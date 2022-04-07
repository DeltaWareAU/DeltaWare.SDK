using System.Text.Json.Serialization;

namespace DeltaWare.SDK.EventBus.Events
{
    public record Event
    {
        public Event()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public Event(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonInclude]
        public Guid Id { get; }

        [JsonInclude]
        public DateTime CreationDate { get; }
    }
}
