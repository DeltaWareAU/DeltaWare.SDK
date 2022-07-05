﻿using DeltaWare.SDK.Serialization.Csv.Attributes;
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

        internal static CsvSchemaException InvalidRecordTypeDeclaration(Type type)
        {
            return new CsvSchemaException($"The specified type {type.Name} cannot be used as a record type schema as it does not declare the {nameof(RecordKeyAttribute)}.");
        }

        public static Exception UndeclaredRecordTypeEncountered(string recordType)
        {
            return new CsvSchemaException($"A schema was not declared for the current Record Type: {recordType}.");
        }
    }
}