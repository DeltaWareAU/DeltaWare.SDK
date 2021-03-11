
using DeltaWare.SDK.Serialization.Arguments.Attributes;
using System.Reflection;

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
