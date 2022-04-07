using System;

namespace DeltaWare.SDK.Serialization.Csv.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnHeaderAttribute : Attribute
    {
        public string HeaderName { get; }

        public ColumnHeaderAttribute(string headerName)
        {
            if (string.IsNullOrEmpty(headerName))
            {
                throw new ArgumentException(nameof(headerName));
            }

            HeaderName = headerName;
        }
    }
}