using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeltaWare.SDK.EntityFrameworkCore.Entities
{
    public abstract class SoftDeletedEntity<TIdentifier> : Entity<TIdentifier> where TIdentifier : struct
    {
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Configures all DB properties for a <see cref="SoftDeletedEntity{TIdentifier}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The model type to be used for representation.</typeparam>
        /// <param name="tableName">The name of the table this Model is representing.</param>
        protected new static EntityTypeBuilder<TEntity> OnModelCreating<TEntity>(ModelBuilder modelBuilder, string tableName) where TEntity : SoftDeletedEntity<TIdentifier>
        {
            EntityTypeBuilder<TEntity> entityBuilder = Entity<TIdentifier>.OnModelCreating<TEntity>(modelBuilder, tableName);

            entityBuilder.Property(e => e.IsDeleted).HasDefaultValue(false).IsRequired();
            entityBuilder.HasQueryFilter(e => e.IsDeleted == false);

            return entityBuilder;
        }
    }
}