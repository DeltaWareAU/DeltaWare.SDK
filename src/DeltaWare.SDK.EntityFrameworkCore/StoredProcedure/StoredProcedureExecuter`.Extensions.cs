using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaWare.SDK.EntityFrameworkCore.StoredProcedure
{
    public static class StoredProcedureExecuterExtensions
    {
        /// <inheritdoc cref="Queryable.FirstOrDefault"/>
        public static TSource FirstOrDefault<TSource>(this IStoredProcedureExecuter<TSource> source) where TSource : class
        {
            return source.Execute().FirstOrDefault();
        }

        /// <inheritdoc cref="EntityFrameworkQueryableExtensions.FirstOrDefaultAsync"/>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IStoredProcedureExecuter<TSource> source) where TSource : class
        {
            return source.Execute().FirstOrDefaultAsync();
        }

        /// <inheritdoc cref="Queryable.SingleOrDefault"/>
        public static TEntity SingleOrDefault<TEntity>(this IStoredProcedureExecuter<TEntity> executer) where TEntity : class
        {
            return executer.Execute().SingleOrDefault();
        }

        /// <inheritdoc cref="EntityFrameworkQueryableExtensions.SingleOrDefaultAsync"/>
        public static Task<TEntity> SingleOrDefaultAsync<TEntity>(this IStoredProcedureExecuter<TEntity> executer) where TEntity : class
        {
            return executer.Execute().SingleOrDefaultAsync();
        }

        /// <inheritdoc cref="Enumerable.ToArray"/>
        public static TEntity[] ToArray<TEntity>(this IStoredProcedureExecuter<TEntity> executer) where TEntity : class
        {
            return executer.Execute().ToArray();
        }

        /// <inheritdoc cref="EntityFrameworkQueryableExtensions.ToArrayAsync"/>
        public static Task<TEntity[]> ToArrayAsync<TEntity>(this IStoredProcedureExecuter<TEntity> executer) where TEntity : class
        {
            return executer.Execute().ToArrayAsync();
        }

        /// <inheritdoc cref="Enumerable.ToList"/>
        public static List<TEntity> ToList<TEntity>(this IStoredProcedureExecuter<TEntity> executer) where TEntity : class
        {
            return executer.Execute().ToList();
        }

        /// <inheritdoc cref="EntityFrameworkQueryableExtensions.ToListAsync"/>
        public static Task<List<TEntity>> ToListAsync<TEntity>(this IStoredProcedureExecuter<TEntity> executer) where TEntity : class
        {
            return executer.Execute().ToListAsync();
        }
    }
}