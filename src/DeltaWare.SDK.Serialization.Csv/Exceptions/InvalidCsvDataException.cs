﻿using System;

namespace DeltaWare.SDK.Serialization.Csv.Exceptions
{
    public class InvalidCsvDataException : Exception
    {
        private InvalidCsvDataException(int lineNumber, int linePosition, string message) : base($"[Line:{lineNumber}/Position:{linePosition}] {message}")
        {
        }

        public static InvalidCsvDataException EncapsulationFieldTerminationExpected(int lineNumber, int linePosition)
        {
            return new InvalidCsvDataException(lineNumber, linePosition, "An encapsulated field was not terminated correctly.");
        }

        public static InvalidCsvDataException EncapsulationFieldTerminationExpectedEndOfFile(int lineNumber, int linePosition)
        {
            return new InvalidCsvDataException(lineNumber, linePosition, "An encapsulated field could was not terminated before end of file was reached.");
        }

        public static InvalidCsvDataException ExpectedLineFeed(int lineNumber, int linePosition)
        {
            return new InvalidCsvDataException(lineNumber, linePosition, "A Line Feed was expected but was not found.");
        }

        public static InvalidCsvDataException IllegalCharacterInNonEncapsulatedField(int lineNumber, int linePosition)
        {
            return new InvalidCsvDataException(lineNumber, linePosition, "An Illegal character was found in a non encapsulated field.");
        }

        public static InvalidCsvDataException InvalidColumnCount(int lineNumber, int expected, int actual)
        {
            return new InvalidCsvDataException(lineNumber, 0, $"The current line has an invalid row count. Expected: {expected}, found: {actual}");
        }
    }
}