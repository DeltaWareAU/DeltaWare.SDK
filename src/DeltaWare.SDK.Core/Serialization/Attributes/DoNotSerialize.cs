using DeltaWare.SDK.Core.Serialization.Enums;
using System;

namespace DeltaWare.SDK.Core.Serialization.Attributes
{
    /// <summary>
    /// Specifies that the current property should not be serialized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DoNotSerialize : Attribute
    {
        /// <summary>
        /// Specifies the direction in which the property will not be serialized.
        /// </summary>
        public SerializationDirection Direction { get; }

        public DoNotSerialize(SerializationDirection direction = SerializationDirection.Deserialization | SerializationDirection.Serialization)
        {
            Direction = direction;
        }
    }
}