
namespace DeltaWare.Tools.Serialization.Csv.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ColumnName : Attribute
    {
        internal string Name;

        public ColumnName(string name)
        {
            Name = name;
        }
    }
}
