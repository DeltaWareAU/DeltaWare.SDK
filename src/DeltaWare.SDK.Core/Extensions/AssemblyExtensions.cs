using System.Linq;

// ReSharper disable once CheckNamespace
namespace System.Reflection
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the loaded types from an <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns>All loaded Types from the <see cref="Assembly"/>.</returns>
        public static Type[] GetLoadedTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null).ToArray();
            }
        }
    }
}