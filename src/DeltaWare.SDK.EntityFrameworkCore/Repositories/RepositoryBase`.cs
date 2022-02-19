﻿using DeltaWare.SDK.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaWare.SDK.EntityFrameworkCore.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public DbSet<TEntity> Entities { get; }

        protected RepositoryBase(DbSet<TEntity> entities)
        {
            Entities = entities ?? throw new ArgumentNullException(nameof(entities));
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            EntityEntry<TEntity> newEntity = await Entities.AddAsync(entity);

            return newEntity.Entity;
        }

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return Entities.AddRangeAsync(entities);
        }

        public virtual Task<int> CountAsync()
        {
            return Entities.CountAsync();
        }

        public virtual List<TEntity> Get()
        {
            return Entities.ToList();
        }

        public virtual Task<List<TEntity>> GetAsync()
        {
            return Entities.ToListAsync();
        }

        public virtual void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        public virtual TEntity Update(TEntity entity)
        {
            EntityEntry<TEntity> updatedEntity = Entities.Update(entity);

            return updatedEntity.Entity;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
        }
    }

    public abstract class RepositoryBase<TEntity, TIdentifier> : RepositoryBase<TEntity>, IRepositoryBase<TEntity, TIdentifier> where TEntity : Entity<TIdentifier> where TIdentifier : struct
    {
        protected RepositoryBase(DbSet<TEntity> entities) : base(entities)
        {
        }

        public Task<TEntity> GetAsync(TIdentifier identifier)
        {
            return Entities.SingleOrDefaultAsync(e => e.Id.Equals(identifier));
        }
    }
}