using System;

namespace DeltaWare.SDK.Data
{
    public abstract class Entity<TIdentifier> where TIdentifier : struct
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public TIdentifier Id { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
