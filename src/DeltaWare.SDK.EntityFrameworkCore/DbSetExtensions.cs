using DeltaWare.SDK.Core.Helpers;
using DeltaWare.SDK.EntityFrameworkCore.StoredProcedure;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DeltaWare.SDK.Data.Paging;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    public static class DbSetExtensions
    {
        /// <summary>
        /// Runs the specified stored procedure.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure to be executed</param>
        public static IStoredProcedureFactory<TEntity> RunStoredProcedure<TEntity>(this DbSet<TEntity> entities, string storedProcedure) where TEntity : class
        {
            return new StoredProcedureFactory<TEntity>(entities, storedProcedure);
        }

        public static IPagedResult<TEntity> ToPaged<TEntity>(this DbSet<TEntity> entities, IPagedQuery query, Expression<Func<TEntity, object>> propertySelector, Expression<Func<TEntity, object>> defaultKeySelector) where TEntity : class
        {
            if (!PropertySelectorHelper.GetSelectedProperties(propertySelector).Any())
            {
                throw new ArgumentException("Properties must be selected.");
            }

            Expression<Func<TEntity, object>> keySelector = query.BuildKeySelectorExpression(defaultKeySelector);
            Expression<Func<TEntity, bool>> predicate = ExpressionFactory.BuildPredicateExpression(propertySelector, query.SearchString);

            int skippedItems = query.PageIndex * query.PageItems;
            int totalEntities = entities.Count();
            int filteredEntities = entities.Count(predicate);

            TEntity[] filterEntities;

            if (query.SortDescending)
            {
                filterEntities = entities
                    .AsNoTracking()
                    .OrderByDescending(keySelector)
                    .Where(predicate)
                    .Skip(skippedItems)
                    .Take(query.PageItems)
                    .ToArray();
            }
            else
            {
                filterEntities = entities
                    .AsNoTracking()
                    .OrderBy(keySelector)
                    .Where(predicate)
                    .Skip(skippedItems)
                    .Take(query.PageItems)
                    .ToArray();
            }

            return new PagedResult<TEntity>(filterEntities, totalEntities, filteredEntities);
        }

        public static async Task<IPagedResult<TEntity>> ToPagedAsync<TEntity>(this DbSet<TEntity> entities, IPagedQuery query, Expression<Func<TEntity, object>> propertySelector, Expression<Func<TEntity, object>> defaultKeySelector) where TEntity : class
        {
            if (!PropertySelectorHelper.GetSelectedProperties(propertySelector).Any())
            {
                throw new ArgumentException("Properties must be selected.");
            }

            Expression<Func<TEntity, object>> keySelector = query.BuildKeySelectorExpression(defaultKeySelector);
            Expression<Func<TEntity, bool>> predicate = ExpressionFactory.BuildPredicateExpression(propertySelector, query.SearchString);

            int skippedItems = query.PageIndex * query.PageItems;
            int totalEntities = await entities.CountAsync();
            int filteredEntities = await entities.CountAsync(predicate);

            TEntity[] filterEntities;

            if (query.SortDescending)
            {
                filterEntities = await entities
                    .AsNoTracking()
                    .OrderByDescending(keySelector)
                    .Where(predicate)
                    .Skip(skippedItems)
                    .Take(query.PageItems)
                    .ToArrayAsync();
            }
            else
            {
                filterEntities = await entities
                    .AsNoTracking()
                    .OrderBy(keySelector)
                    .Where(predicate)
                    .Skip(skippedItems)
                    .Take(query.PageItems)
                    .ToArrayAsync();
            }

            return new PagedResult<TEntity>(filterEntities, totalEntities, filteredEntities);
        }
    }
}