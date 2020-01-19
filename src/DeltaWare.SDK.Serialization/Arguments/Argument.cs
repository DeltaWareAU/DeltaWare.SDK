
using System.Reflection;

using DeltaWare.SDK.Serialization.Arguments.Attributes;

namespace DeltaWare.SDK.Serialization.Arguments
{
    internal struct Argument
    {
        public readonly ArgumentBase ArgumentType;

        public readonly PropertyInfo Property;

        public Argument(ArgumentBase argumentType, PropertyInfo property)
        {
            ArgumentType = argumentType;
            Property = property;
        }
    }
}
