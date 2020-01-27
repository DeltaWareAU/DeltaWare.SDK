using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaWare.SDK.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(long id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities);

        Task<TEntity> RemoveAsync(long id);
    }
}
