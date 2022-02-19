using DeltaWare.SDK.Core.Identity;
using DeltaWare.SDK.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeltaWare.SDK.EntityFrameworkCore
{
    /// <summary>
    /// When used in conjunction with <see cref="SoftDeletedEntity{TIdentifier}"/> will soft delete
    /// entities. This is done by marking a Deleted field to true. This means ALL data is kept even
    /// when deleted.
    /// </summary>
    public abstract class SoftDeleteDbContext<TIdentifier> : DbContext where TIdentifier : struct
    {
        protected IIdentityService IdentityService { get; }

        protected SoftDeleteDbContext(DbContextOptions options, IIdentityService identityService = null) : base(options)
        {
            IdentityService = identityService;
        }

        public override int SaveChanges()
        {
            InternalSaveChanges();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            InternalSaveChanges();

            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the <see cref="EntityState"/> of all changed entities to Detached.
        /// </summary>
        protected void DetachChangedEntities()
        {
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }

        protected virtual void InternalSaveChanges()
        {
            string username = IdentityService?.GetCurrentUser() ?? "NOT CONFIGURED";

            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.Entity is SoftDeletedEntity<TIdentifier> softDeletedEntity)
                {
                    UpdateSoftDeletedEntity(entry, softDeletedEntity, username);
                }
            }
        }

        protected virtual void UpdateSoftDeletedEntity(EntityEntry entry, SoftDeletedEntity<TIdentifier> softDeletedEntity, string username)
        {
            DateTime currentDateTime = DateTime.Now;

            switch (entry.State)
            {
                case EntityState.Added:
                    softDeletedEntity.CreatedBy = username;
                    softDeletedEntity.CreatedDate = currentDateTime;
                    softDeletedEntity.ModifiedBy = username;
                    softDeletedEntity.ModifiedDate = currentDateTime;
                    break;

                case EntityState.Modified:
                    softDeletedEntity.ModifiedBy = username;
                    softDeletedEntity.ModifiedDate = currentDateTime;
                    break;

                case EntityState.Deleted:
                    softDeletedEntity.IsDeleted = true;
                    softDeletedEntity.ModifiedBy = username;
                    softDeletedEntity.ModifiedDate = currentDateTime;
                    entry.State = EntityState.Modified;
                    break;
            }
        }
    }
}