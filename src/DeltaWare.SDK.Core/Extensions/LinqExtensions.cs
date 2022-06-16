using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace System.Linq
{
    public static class LinqExtensions
    {
        public static bool Contains(this IEnumerable<string> source, string value, StringComparison comparison)
        {
            foreach (string sourceString in source)
            {
                if (sourceString.IndexOf(value, comparison) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static async Task<T[]> ToArrayAsync<T>(this Task<IEnumerable<T>> enumerable)
        {
            return (await enumerable).ToArray();
        }

        public static async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> enumerable)
        {
            return (await enumerable).ToList();
        }

        public static async Task<IEnumerable<T>> CastAsync<T>(this Task<IEnumerable> enumerable)
        {
            return (await enumerable).Cast<T>();
        }

        public static async Task<IEnumerable<T>> CastAsync<T>(this Task<IEnumerable<object>> enumerable)
        {
            return (await enumerable).Cast<T>();
        }
    }
}