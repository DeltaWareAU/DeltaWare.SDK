using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeltaWare.SDK.DataAccess.Interfaces
{
    public interface IQueryableRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
