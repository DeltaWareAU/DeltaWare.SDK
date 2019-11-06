
namespace DeltaWare.SDK.Serialization.Arguments.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ArgumentBase : Attribute
    {
        public string Name { get; internal set; }

        protected ArgumentBase()
        {
        }

        protected ArgumentBase(string name)
        {
            this.Name = name;
        }
    }
}
