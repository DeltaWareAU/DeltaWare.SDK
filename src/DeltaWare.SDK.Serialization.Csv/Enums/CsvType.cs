using DeltaWare.SDK.Serialization.Csv.Writing;

namespace DeltaWare.SDK.Serialization.Csv.Enums
{
    /// <summary>
    /// Specifies how the <see cref="CsvWriter"/> will write the CSV data.
    /// </summary>
    public enum CsvType
    {
        /// <summary>
        /// Default for writing CSV data.
        /// </summary>
        Default,
        /// <summary>
        /// Enables support for writing Record Type CSV data, this disables checks performed to check for CSV data validity.
        /// </summary>
        Record
    }
}
