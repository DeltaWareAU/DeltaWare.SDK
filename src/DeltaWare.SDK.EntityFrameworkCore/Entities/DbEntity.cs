using DeltaWare.SDK.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeltaWare.SDK.EntityFrameworkCore.Entities
{
    public abstract class DbEntity<TIdentifier> : Entity<TIdentifier> where TIdentifier : struct
    {
        /// <summary>
        /// Configures all DB properties for a <see cref="DbEntity{TIdentifier}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The model type to be used for representation.</typeparam>
        /// <param name="tableName">The name of the table this Model is representing.</param>
        protected static EntityTypeBuilder<TEntity> OnModelCreating<TEntity>(ModelBuilder modelBuilder, string tableName) where TEntity : DbEntity<TIdentifier>
        {
            EntityTypeBuilder<TEntity> entityBuilder = modelBuilder.Entity<TEntity>();

            entityBuilder.ToTable(tableName);

            entityBuilder.HasKey(e => e.Id);

            entityBuilder.Property(e => e.CreatedDate).IsRequired();
            entityBuilder.Property(e => e.CreatedBy).IsRequired();
            entityBuilder.Property(e => e.ModifiedDate).IsRequired();
            entityBuilder.Property(e => e.ModifiedBy).IsRequired();

            return entityBuilder;
        }
    }
}