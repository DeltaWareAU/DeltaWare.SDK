using System;

namespace DeltaWare.SDK.Serialization.Csv.Exceptions
{
    public class CsvSchemaException : Exception
    {
        private CsvSchemaException(string message) : base(message)
        {
        }

        internal static CsvSchemaException DuplicateProperties(int columnIndex, string indexedPropertyName, string propertyName)
        {
            return new CsvSchemaException($"Duplicate Properties assigned to index[{columnIndex}]. First[{indexedPropertyName}] Second[{propertyName}]");
        }
    }
}