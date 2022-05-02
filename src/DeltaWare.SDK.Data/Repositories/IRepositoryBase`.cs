using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Data.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        int Count();
        List<TEntity> Get();

        void Remove(TEntity entity);


        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<int> CountAsync();

        Task<List<TEntity>> GetAsync();

        Task RemoveAsync(TEntity entity);

        Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    }

    public interface IRepositoryBase<TEntity, in TIdentifier> : IRepositoryBase<TEntity> where TIdentifier : struct where TEntity : Entity<TIdentifier>
    {
        TEntity Get(TIdentifier identifier);
        Task<TEntity> GetAsync(TIdentifier identifier);
    }
}