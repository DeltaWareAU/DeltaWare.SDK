using DeltaWare.SDK.Serialization.Csv.Exceptions;

namespace DeltaWare.SDK.Serialization.Csv.Enums
{
    /// <summary>
    /// Specifies how a missing column will be handled.
    /// </summary>
    public enum MissingColumnHandling
    {
        /// <summary>
        /// When a line has missing columns they will be ignored.
        /// </summary>
        NotAllow,

        /// <summary>
        /// When a line has a missing columns an <see cref="InvalidCsvDataException"/> will be thrown.
        /// </summary>
        Allow
    }
}