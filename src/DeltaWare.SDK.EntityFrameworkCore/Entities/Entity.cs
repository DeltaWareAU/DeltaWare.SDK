using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DeltaWare.SDK.EntityFrameworkCore.Entities
{
    public abstract class Entity<TIdentifier> where TIdentifier : struct
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public TIdentifier Id { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Configures all DB properties for a <see cref="Entity{TIdentifier}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The model type to be used for representation.</typeparam>
        /// <param name="tableName">The name of the table this Model is representing.</param>
        protected static EntityTypeBuilder<TEntity> OnModelCreating<TEntity>(ModelBuilder modelBuilder, string tableName) where TEntity : Entity<TIdentifier>
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