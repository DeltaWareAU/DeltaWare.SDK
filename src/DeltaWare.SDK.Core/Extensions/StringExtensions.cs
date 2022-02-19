using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a copy of the string where the first character is upper case.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when an empty string is provided.</exception>
        public static string FirstToUpper(this string value)
        {
            if (value == string.Empty)
            {
                throw new ArgumentException($"{nameof(value)} cannot be empty", nameof(value));
            }

            string firstCharacter = value[0].ToString().ToUpper();

            if (value.Length == 1)
            {
                return firstCharacter;
            }

            return firstCharacter + value[1..];
        }

        /// <summary>
        /// Removes all non numerical values from the string. Order of characters is kept.
        /// </summary>
        /// <param name="allowedCharacters">Specifies the non numeric characters to be kept.</param>
        /// <returns>
        /// A new value that does not contain non numerical values or that are not allowed characters.
        /// </returns>
        public static string RemoveNonNumerical(this string value, params char[] allowedCharacters)
        {
            return new string(value.Where(c => char.IsDigit(c) || allowedCharacters.Contains(c)).ToArray());
        }

        /// <summary>
        /// Splits the string into chunks.
        /// </summary>
        /// <param name="str">The string to be split.</param>
        /// <param name="chunkSize">The size of the chunks.</param>
        /// <returns>
        /// Returns a string split into chunks, EG ABCDEFGHIJK with a chunk size of 3 would be ABC,DEF,GHI,JK
        /// </returns>
        public static IEnumerable<string> Split(this string str, int chunkSize)
        {
            return Enumerable
                .Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}