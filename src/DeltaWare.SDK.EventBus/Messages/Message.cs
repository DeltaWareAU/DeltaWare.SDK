using System;
using System.Text.Json.Serialization;

namespace DeltaWare.SDK.MessageBroker.Messages
{
    public record Message
    {
        public Message()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public Message(Guid id, DateTime createDate)
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
