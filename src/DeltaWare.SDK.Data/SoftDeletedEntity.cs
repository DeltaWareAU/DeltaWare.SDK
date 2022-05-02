namespace DeltaWare.SDK.Data
{
    public abstract class SoftDeletedEntity<TIdentifier> : Entity<TIdentifier> where TIdentifier : struct
    {
        public bool IsDeleted { get; set; }
    }
}
