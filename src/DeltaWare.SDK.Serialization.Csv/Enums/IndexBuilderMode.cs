namespace DeltaWare.SDK.Serialization.Csv.Enums
{
    /// <summary>
    /// Specifies what the <see cref="Old.CsvSerializer"/> will use to build the PropertyIndex.
    /// </summary>
    internal enum IndexBuilderMode
    {
        /// <summary>
        /// Signifies that no mode has been selected.
        /// </summary>
        None,

        /// <summary>
        /// Signifies the the properties attributes be will used.
        /// </summary>
        Attribute,

        /// <summary>
        /// Signifies that the properties name or index will be used.
        /// </summary>
        Property
    }
}