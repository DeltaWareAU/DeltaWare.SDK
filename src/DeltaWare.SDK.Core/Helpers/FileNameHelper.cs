using DeltaWare.SDK.Core.Validators;
using System;

namespace DeltaWare.SDK.Core.Helpers
{
    public static class FileNameHelper
    {
        /// <summary>
        /// Replaces the extensions of the provided file with the specified extension.
        /// </summary>
        /// <param name="filePath">The file to have its extension replaced.</param>
        /// <param name="fileExtension">The extension that will replace the current extension.</param>
        /// <returns>Returns a new file path where the file extension has been replaced.</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public static string ReplaceExtension(string filePath, string fileExtension)
        {
            StringValidator.ThrowOnNullOrWhitespace(filePath, nameof(filePath));
            StringValidator.ThrowOnNullOrWhitespace(fileExtension, nameof(fileExtension));

            if (fileExtension.StartsWith('.'))
            {
                throw new ArgumentException("File Extension must not start with a period");
            }

            int fileExtensionIndex = filePath.LastIndexOf('.');

            filePath = filePath.Substring(0, fileExtensionIndex);
            filePath += $".{fileExtension}";

            return filePath;
        }
    }
}