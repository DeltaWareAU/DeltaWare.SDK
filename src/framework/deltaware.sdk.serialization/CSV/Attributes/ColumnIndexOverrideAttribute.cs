
namespace DeltaWare.SDK.Serialization.CSV.Attributes
{
    using System;

    public class ColumnIndexOverrideAttribute : Attribute
    {
        internal int Id;

        public ColumnIndexOverrideAttribute(int id)
        {
            Id = id;
        }
    }
}
