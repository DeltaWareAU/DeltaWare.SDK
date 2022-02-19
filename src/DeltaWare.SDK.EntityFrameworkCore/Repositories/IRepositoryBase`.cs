using DeltaWare.SDK.EntityFrameworkCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaWare.SDK.EntityFrameworkCore.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<int> CountAsync();

        Task<List<TEntity>> GetAsync();

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);
    }

    public interface IRepositoryBase<TEntity, in TIdentifier> : IRepositoryBase<TEntity> where TIdentifier : struct where TEntity : Entity<TIdentifier>
    {
        Task<TEntity> GetAsync(TIdentifier identifier);
    }
}