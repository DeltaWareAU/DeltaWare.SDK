using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaWare.SDK.DataAccess.Interfaces
{
    public interface IIdentityRepository<TEntity> where TEntity : IIdentityEntity
    {
        Task<IEnumerable<TEntity>> GetAsync();

        Task<TEntity> GetAsync(Guid identity);

        Task<TEntity> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities);

        Task<TEntity> RemoveAsync(Guid identity);
    }
}
