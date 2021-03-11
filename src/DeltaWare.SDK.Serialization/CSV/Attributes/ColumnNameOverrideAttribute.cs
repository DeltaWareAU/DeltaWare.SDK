
namespace DeltaWare.SDK.Serialization.CSV.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ColumnNameOverrideAttribute: Attribute
    {
        internal string Name;

        public ColumnNameOverrideAttribute(string name)
        {
            Name = name;
        }
    }
}
