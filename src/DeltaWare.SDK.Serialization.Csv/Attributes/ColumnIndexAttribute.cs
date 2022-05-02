using System;

namespace DeltaWare.SDK.Serialization.Csv.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnIndexAttribute : Attribute
    {
        public int Index { get; }

        public ColumnIndexAttribute(int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Index must be a positive number.", nameof(index));
            }

            Index = index;
        }
    }
}