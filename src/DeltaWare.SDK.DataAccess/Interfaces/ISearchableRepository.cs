using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaWare.SDK.DataAccess.Interfaces
{
    public interface ISearchableRepository<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> SearchAsync(TEntity searchEntity);
    }
}
