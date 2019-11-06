
namespace DeltaWare.SDK.Serialization.Csv.Attributes
{
    using System;

    public class ColumnIndex : Attribute
    {
        internal int Id;

        public ColumnIndex(int id)
        {
            Id = id;
        }
    }
}
