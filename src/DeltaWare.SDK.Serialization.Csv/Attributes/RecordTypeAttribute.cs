using DeltaWare.SDK.Core.Validators;
using System;

namespace DeltaWare.SDK.Serialization.Csv.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RecordTypeAttribute : Attribute
    {
        public string Type { get; }

        public RecordTypeAttribute(string type)
        {
            StringValidator.ThrowOnNullOrWhitespace(type, nameof(type));

            Type = type;
        }
    }
}
