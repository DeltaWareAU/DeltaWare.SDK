using System.Linq;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class StringHelper
    {
        /// <summary>
        /// Checks if the specified strings are all null or empty.
        /// </summary>
        /// <param name="strings">An array of string to be checked.</param>
        /// <returns>True if all strings are null or empty.</returns>
        public static bool AllAreNullOrEmpty(params string[] strings)
        {
            return strings.All(string.IsNullOrEmpty);
        }

        /// <summary>
        /// Checks if the specified strings are all null or white space.
        /// </summary>
        /// <param name="strings">An array of string to be checked.</param>
        /// <returns>True if all strings are null or white space.</returns>
        public static bool AllAreNullOrWhiteSpace(params string[] strings)
        {
            return strings.All(string.IsNullOrWhiteSpace);
        }

        /// <summary>
        /// Checks if any of the specified strings are null or empty.
        /// </summary>
        /// <param name="strings">An array of string to be checked.</param>
        /// <returns>True if any of strings are null or empty.</returns>
        public static bool AnyAreNullOrEmpty(params string[] strings)
        {
            return strings.Any(string.IsNullOrEmpty);
        }

        /// <summary>
        /// Checks if any of the specified strings are null or white space.
        /// </summary>
        /// <param name="strings">An array of string to be checked.</param>
        /// <returns>True if any of strings are null or white space.</returns>
        public static bool AnyAreNullOrWhiteSpace(params string[] strings)
        {
            return strings.Any(string.IsNullOrWhiteSpace);
        }
    }
}