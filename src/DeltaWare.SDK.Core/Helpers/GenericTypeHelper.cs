using System;
using System.Collections;
using System.Collections.Generic;

namespace DeltaWare.SDK.Core.Helpers
{
    public static class GenericTypeHelper
    {
        /// <summary>
        /// Creates an <see cref="IList"/> for the specified type.
        /// </summary>
        /// <param name="type">The type to be stored in the list.</param>
        /// <returns>A new <see cref="IList"/> where its generic type is the specified type.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static IList CreateGenericList(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type genericListType = typeof(List<>).MakeGenericType(type);

            return (IList)Activator.CreateInstance(genericListType);
        }
    }
}