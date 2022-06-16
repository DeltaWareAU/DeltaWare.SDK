using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class TypeExtensions
    {
        public static PropertyInfo[] GetPublicProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Indicates if the current type implement the current interface.
        /// </summary>
        /// <typeparam name="T">The interface to be checked.</typeparam>
        /// <param name="type">The type to be checked.</param>
        /// <returns><see langword="true"/> if the type implement the specified interface.</returns>
        public static bool ImplementsInterface<T>(this Type type) where T : class
        {
            return type.GetInterfaces().Contains(typeof(T));
        }

        public static bool ImplementsAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute<T>() != null;
        }

        public static bool IsSubclassOf<T>(this Type type)
        {
            return type.IsSubclassOf(typeof(T));
        }

        public static bool IsSubclassOfRawGeneric(this Type type, Type genericType)
        {
            if (!genericType.IsGenericType)
            {
                throw new ArgumentException();
            }

            while (type.BaseType != null && type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }

                if (type == genericType)
                {
                    return true;
                }

                type = type.BaseType;
            }

            return false;
        }

        public static Type[] GetGenericArguments(this Type type, Type genericType)
        {
            if (!genericType.IsGenericType)
            {
                throw new ArgumentException();
            }

            while (type.BaseType != null || type == typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
                {
                    return type.GetGenericArguments();
                }

                type = type.BaseType;
            }

            return Array.Empty<Type>();
        }
    }
}