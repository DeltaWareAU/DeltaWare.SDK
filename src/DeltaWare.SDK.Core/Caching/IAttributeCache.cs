using System;
using System.Reflection;

namespace DeltaWare.SDK.Core.Caching
{
    public interface IAttributeCache
    {
        TAttribute[] GetAttributes<TAttribute>(Type type) where TAttribute : Attribute;

        TAttribute[] GetAttributes<TAttribute>(PropertyInfo property) where TAttribute : Attribute;

        bool HasAttribute<TAttribute>(Type type) where TAttribute : Attribute;

        bool HasAttribute<TAttribute>(PropertyInfo property) where TAttribute : Attribute;

        bool TryGetAttribute<TAttribute>(Type type, out TAttribute attribute) where TAttribute : Attribute;

        bool TryGetAttribute<TAttribute>(PropertyInfo property, out TAttribute attribute) where TAttribute : Attribute;

        bool TryGetAttributes<TAttribute>(PropertyInfo property, out TAttribute[] attributes) where TAttribute : Attribute;

        bool TryGetAttributes<TAttribute>(Type type, out TAttribute[] attributes) where TAttribute : Attribute;
    }
}