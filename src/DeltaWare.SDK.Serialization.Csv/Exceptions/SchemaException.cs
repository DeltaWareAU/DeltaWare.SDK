using System;

namespace DeltaWare.SDK.Serialization.Csv.Exceptions
{
    public class SchemaException : Exception
    {
        public SchemaException(string message) : base(message)
        {
        }
    }
}