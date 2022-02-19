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

        public static bool ImplementsInterface<T>(this Type type) where T : class
        {
            return type.GetInterfaces().Contains(typeof(T));
        }
    }
}