using System;
using System.Linq.Expressions;
using DeltaWare.SDK.Core.Helpers;

namespace DeltaWare.SDK.Data.Paging
{
    public static class PagedQueryExtensions
    {
        /// <summary>
        /// Gets an Expression that can be used by linq to sort a collection.
        /// </summary>
        /// <typeparam name="TSource">The type to be sorted.</typeparam>
        /// <param name="defaultExpression">
        /// The default expression to be used when no sort string is specified.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <see cref="IPagedQuery"/> does not contain a sort string and no default
        /// expression is provided.
        /// </exception>
        /// <returns>Returns an <see cref="Expression"/> than can be used to sort.</returns>
        public static Expression<Func<TSource, object>> BuildKeySelectorExpression<TSource>(this IPagedQuery query, Expression<Func<TSource, object>> defaultExpression = null)
        {
            if (query.SortString == null)
            {
                if (defaultExpression != null)
                {
                    return defaultExpression;
                }

                throw new ArgumentNullException(nameof(query.SortString), "BuildKeySelectorExpression cannot be used if SortString is null.");
            }

            return ExpressionFactory.GetKeySelectorExpression<TSource>(query.SortString);
        }

        /// <summary>
        /// Gets a Function that can be used by linq to sort a collection.
        /// </summary>
        /// <typeparam name="TSource">The type to be sorted.</typeparam>
        /// <param name="defaultExpression">
        /// The default expression to be used when no sort string is specified.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <see cref="IPagedQuery"/> does not contain a sort string and no default
        /// expression is provided.
        /// </exception>
        /// <returns>Returns an <see cref="Expression"/> than can be used to sort.</returns>
        public static Func<TSource, object> BuildKeySelectorFunc<TSource>(this IPagedQuery query, Func<TSource, object> defaultExpression = null)
        {
            if (query.SortString == null)
            {
                if (defaultExpression != null)
                {
                    return defaultExpression;
                }

                throw new ArgumentNullException(nameof(query.SortString), "BuildKeySelectorExpression cannot be used if SortString is null.");
            }

            return ExpressionFactory.GetKeySelectorExpression<TSource>(query.SortString).Compile();
        }
    }
}