using System;

namespace DeltaWare.SDK.Core.Validators
{
    /// <summary>
    /// Provides helpful functions for validating strings.
    /// </summary>
    public static class StringValidator
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the value is null or; Throw a <see
        /// cref="ArgumentException"/> if the value is empty or whitespace.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <param name="paramName">The parameter name to be appended to the exception.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public static void ThrowOnNullOrWhitespace(string value, string paramName = null)
        {
            paramName ??= "Value";

            if (value == null)
            {
                throw new ArgumentNullException(paramName, $"{paramName} cannot be null");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{paramName} cannot be empty or consist only of whitespace characters");
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the value is null or; Throw a <see
        /// cref="ArgumentException"/> if the value is empty.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <param name="paramName">The parameter name to be appended to the exception.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public static void ThrowOnNullOrEmpty(string value, string paramName = null)
        {
            paramName ??= "Value";

            if (value == null)
            {
                throw new ArgumentNullException(paramName, $"{paramName} cannot be null");
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{paramName} cannot be empty or consist only of whitespace characters");
            }
        }
    }
}