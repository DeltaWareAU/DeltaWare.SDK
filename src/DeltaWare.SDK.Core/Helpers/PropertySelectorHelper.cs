using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace DeltaWare.SDK.Core.Helpers
{
    /// <summary>
    /// Provides useful functions to select properties from a type.
    /// </summary>
    public static class PropertySelectorHelper
    {
        /// <summary>
        /// Gets the selected properties from the specified type.
        /// </summary>
        /// <typeparam name="T">The type where the properties will be retrieved from.</typeparam>
        /// <param name="propertySelector">Selected the properties from the specified type.</param>
        /// <returns>
        /// Returns an <see cref="IEnumerable{T}"/> of <see cref="PropertyInfo"/> from the specified type.
        /// </returns>
        /// <exception cref="ArgumentException"/>
        public static IEnumerable<PropertyInfo> GetSelectedProperties<T>(Expression<Func<T, object>> propertySelector) where T : class
        {
            if (propertySelector.Body is not NewExpression selectorBody)
            {
                throw new ArgumentException($"{nameof(propertySelector)} must be a {nameof(MemberExpression)}");
            }

            foreach (Expression selectorArguments in selectorBody.Arguments)
            {
                if (selectorArguments is MemberExpression selectedProperty)
                {
                    yield return (PropertyInfo)selectedProperty.Member;
                }
            }
        }
    }
}