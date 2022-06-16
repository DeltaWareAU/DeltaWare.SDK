using DeltaWare.SDK.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Data.Paging
{
    public static class ToPagedExtensions
    {
        /// <summary>
        /// Executes the specified <see cref="IPagedQuery"/> on the collection.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> contained in the collection.</typeparam>
        /// <param name="collection">The collection for the <see cref="IPagedQuery"/> to be executed against.</param>
        /// <param name="propertySelector">The query to be performed.</param>
        /// <param name="defaultOrderKey">Selects the properties for the query to be executed against.</param>
        /// <param name="defaultOrderKey">Selects the default property to order the results.</param>
        /// <returns>A <see cref="IPagedQuery"/> containing the results of the <see cref="IPagedQuery"/>.</returns>
        public static IPagedResult<T> ToPaged<T>(this IEnumerable<T> collection, IPagedQuery query, Expression<Func<T, object>> propertySelector, Expression<Func<T, object>> defaultOrderKey) where T : class
        {
            return InternalToPaged(collection.ToArray(), query, propertySelector, defaultOrderKey);
        }

        /// <summary>
        /// Executes the specified <see cref="IPagedQuery"/> on the collection.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> contained in the collection.</typeparam>
        /// <param name="collection">The collection for the <see cref="IPagedQuery"/> to be executed against.</param>
        /// <param name="query">The query to be performed.</param>
        /// <param name="propertySelector">Selects the properties for the query to be executed against.</param>
        /// <param name="defaultOrderKey">Selects the default property to order the results.</param>
        /// <returns>An awaitable <see cref="Task{TResult}"/> containing the results of the <see cref="IPagedQuery"/>.</returns>
        public static async Task<IPagedResult<T>> ToPagedAsync<T>(this Task<IEnumerable<T>> collection, IPagedQuery query, Expression<Func<T, object>> propertySelector, Expression<Func<T, object>> defaultOrderKey) where T : class
        {
            return InternalToPaged(await collection.ToArrayAsync(), query, propertySelector, defaultOrderKey);
        }

        private static IPagedResult<T> InternalToPaged<T>(this T[] collection, IPagedQuery query, Expression<Func<T, object>> propertySelector, Expression<Func<T, object>> defaultOrderKey) where T : class
        {
            if (!PropertySelectorHelper.GetSelectedProperties(propertySelector).Any())
            {
                throw new ArgumentException("Properties must be selected.");
            }

            Func<T, object> keySelector = query.BuildKeySelectorExpression(defaultOrderKey).Compile();
            Func<T, bool> predicate = ExpressionFactory.BuildPredicateExpression(propertySelector, query.SearchString).Compile();

            int skippedItems = query.PageIndex * query.PageItems;
            int totalEntities = collection.Length;
            int filteredEntities = collection.Count(predicate);

            T[] filterEntities;

            if (query.SortDescending)
            {
                filterEntities = collection
                    .OrderByDescending(keySelector)
                    .Where(predicate)
                    .Skip(skippedItems)
                    .Take(query.PageItems)
                    .ToArray();
            }
            else
            {
                filterEntities = collection
                    .OrderBy(keySelector)
                    .Where(predicate)
                    .Skip(skippedItems)
                    .Take(query.PageItems)
                    .ToArray();
            }

            return new PagedResult<T>(filterEntities, totalEntities, filteredEntities);
        }
    }
}
