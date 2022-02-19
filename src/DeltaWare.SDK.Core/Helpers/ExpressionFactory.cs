using DeltaWare.SDK.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DeltaWare.SDK.Core.Helpers
{
    /// <summary>
    /// Provides useful functions for building an <see cref="Expression"/>.
    /// </summary>
    public static class ExpressionFactory
    {
        /// <summary>
        /// Builds an <see cref="Expression"/> that can be used to order a list by the selected
        /// properties and the filter.
        /// </summary>
        /// <typeparam name="T">The type the <see cref="Expression"/> is built for.</typeparam>
        /// <param name="propertySelector">Selected specified properties from the specified type.</param>
        /// <param name="filter">The string to be searched for.</param>
        /// <returns>Returns an <see cref="Expression"/> that performs Trim, ToLower and Contains.</returns>
        /// <remarks>Presently only searching string properties is supported.</remarks>
        /// <exception cref="NotImplementedException">
        /// Thrown when a selected properties type is not of type <see cref="string"/>.
        /// </exception>
        public static Expression<Func<T, bool>> BuildPredicateExpression<T>(Expression<Func<T, object>> propertySelector, string filter) where T : class
        {
            // T is a compile-time placeholder for the element type of the query.
            Type sourceType = typeof(T);

            ParameterExpression sourceParameter = Expression.Parameter(sourceType);

            ConstantExpression searchConstant = Expression.Constant(filter ?? string.Empty, typeof(string));

            List<Expression> expressions = new();

            foreach (PropertyInfo property in PropertySelectorHelper.GetSelectedProperties(propertySelector))
            {
                if (property.PropertyType != typeof(string))
                {
                    throw new NotImplementedException("At present property types that aren't strings are not supported.");
                }

                // The below code generates the following e => e.Trim().ToLower().Contains(filter);

                var propertyExpression = Expression.Property(sourceParameter, property);

                MethodInfo trimMethod = property.PropertyType.GetMethod("Trim", Type.EmptyTypes);
                MethodCallExpression callTrim = Expression.Call(propertyExpression, trimMethod);

                MethodInfo toLowerMethod = property.PropertyType.GetMethod("ToLower", Type.EmptyTypes);
                MethodCallExpression callToLower = Expression.Call(callTrim, toLowerMethod);

                MethodInfo containsMethods = property.PropertyType.GetMethod("Contains", new[] { property.PropertyType });
                MethodCallExpression callContains = Expression.Call(callToLower, containsMethods, searchConstant);

                expressions.Add(callContains);
            }

            // Combines our method calls appending || between them.
            Expression body = expressions.Aggregate(Expression.Or);

            return Expression.Lambda<Func<T, bool>>(body, sourceParameter);
        }

        /// <summary>
        /// Gets an <see cref="Expression"/> that orders a collection using the specified property.
        /// </summary>
        /// <typeparam name="TSource">The type to be ordered.</typeparam>
        /// <param name="propertyName">
        /// Specifies what property the <see cref="Expression"/> will use to order the collection.
        /// </param>
        /// <returns>Returns an <see cref="Expression"/> that orders a collection.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static Expression<Func<TSource, object>> GetKeySelectorExpression<TSource>(string propertyName)
        {
            StringValidator.ThrowOnNullOrWhitespace(propertyName, nameof(propertyName));

            Type sourceType = typeof(TSource);

            PropertyInfo sortProperty = sourceType.GetProperty(propertyName);

            if (sortProperty == null)
            {
                throw new ArgumentNullException(nameof(sortProperty), $"No property with the name {propertyName} could be found in Type {typeof(TSource).Name}");
            }

            ParameterExpression parameterExpression = Expression.Parameter(sourceType);
            MemberExpression propertyExpression = Expression.Property(parameterExpression, sortProperty);
            UnaryExpression unaryExpression = Expression.Convert(propertyExpression, typeof(object));

            return Expression.Lambda<Func<TSource, object>>(unaryExpression, parameterExpression);
        }
    }
}