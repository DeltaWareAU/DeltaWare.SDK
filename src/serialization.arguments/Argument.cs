
namespace DeltaWare.Tools.Serialization.Arguments
{
    using System.Reflection;

    using DeltaWare.Tools.Serialization.Arguments.Attributes;

    internal struct Argument
    {
        public readonly ArgumentBase ArgumentType;

        public readonly PropertyInfo Property;

        public Argument(ArgumentBase argumentType, PropertyInfo property)
        {
            this.ArgumentType = argumentType;
            this.Property = property;
        }
    }
}
