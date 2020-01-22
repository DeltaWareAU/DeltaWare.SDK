using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaWare.SDK.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(long id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        Task AddManyAsync(IEnumerable<TEntity> entity);

        Task RemoveAsync(long id);
    }
}
