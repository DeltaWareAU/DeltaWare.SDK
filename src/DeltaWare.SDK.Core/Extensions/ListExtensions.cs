using DeltaWare.SDK.Core.Helpers;
using DeltaWare.SDK.Core.Paging;
using System.Linq;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
    public static class ListExtensions
    {
        public static bool IsEmpty(this IList value)
        {
            return value.Count == 0;
        }

        /// <summary>
        /// Splits the list into multiple new lists.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="list">The list to be split.</param>
        /// <param name="count">How many lists should be generated.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the count provided is less than or equal to 1.
        /// </exception>
        public static List<T>[] Split<T>(this List<T> list, int count)
        {
            if (count <= 1)
            {
                throw new ArgumentException("Count must be greater than 1");
            }

            List<T>[] lists = new List<T>[count];

            // Instantiate each list.
            for (int i = 0; i < lists.Length; i++)
            {
                lists[i] = new List<T>();
            }

            int listIndex = 0;

            for (int i = 0; i < list.Count; i++)
            {
                lists[listIndex].Add(list[i]);

                listIndex++;

                if (listIndex >= count)
                {
                    listIndex = 0;
                }
            }

            return lists;
        }

        /// <summary>
        /// Splits the list into multiple new lists.
        /// </summary>
        /// <param name="list">The list to be split.</param>
        /// <param name="count">How many lists should be generated.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the count provided is less than or equal to 1.
        /// </exception>
        public static IList[] Split(this IList list, int count)
        {
            if (count <= 1)
            {
                throw new ArgumentException("Count must be greater than 1");
            }

            Type genericType = list.GetType().GenericTypeArguments.First();

            IList[] lists = new IList[count];

            // Instantiate each list.
            for (int i = 0; i < lists.Length; i++)
            {
                lists[i] = GenericTypeHelper.CreateGenericList(genericType);
            }

            int listIndex = 0;

            for (int i = 0; i < list.Count; i++)
            {
                lists[listIndex].Add(list[i]);

                listIndex++;

                if (listIndex >= count)
                {
                    listIndex = 0;
                }
            }

            return lists;
        }

        public static IPagedResult<TEntity> ToPaged<TEntity>(this List<TEntity> entities, IPagedQuery query, Expression<Func<TEntity, object>> propertySelector, Expression<Func<TEntity, object>> defaultKeySelector) where TEntity : class
        {
            if (!PropertySelectorHelper.GetSelectedProperties(propertySelector).Any())
            {
                throw new ArgumentException("Properties must be selected.");
            }

            Func<TEntity, object> keySelector = query.BuildKeySelectorExpression(defaultKeySelector).Compile();
            Func<TEntity, bool> predicate = ExpressionFactory.BuildPredicateExpression(propertySelector, query.SearchString).Compile();

            int skippedItems = query.PageIndex * query.PageItems;
            int totalEntities = entities.Count();
            int filteredEntities = entities.Count(predicate);

            TEntity[] filterEntities;

            if (query.SortDescending)
            {
                filterEntities = entities
                    .OrderByDescending(keySelector)
                    .Where(predicate)
                    .Skip(skippedItems)
                    .Take(query.PageItems)
                    .ToArray();
            }
            else
            {
                filterEntities = entities
                    .OrderBy(keySelector)
                    .Where(predicate)
                    .Skip(skippedItems)
                    .Take(query.PageItems)
                    .ToArray();
            }

            return new PagedResult<TEntity>(filterEntities, totalEntities, filteredEntities);
        }
    }
}