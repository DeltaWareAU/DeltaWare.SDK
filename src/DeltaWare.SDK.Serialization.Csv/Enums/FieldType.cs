namespace DeltaWare.SDK.Serialization.Csv.Enums
{
    /// <summary>
    /// Indicates the Field Type.
    /// </summary>
    public enum FieldType
    {
        /// <summary>
        /// Normal Field.
        /// </summary>
        /// <remarks>Any field that is not the last field for the current line.</remarks>
        Field,
        /// <summary>
        /// End Field.
        /// </summary>
        /// <remarks>The last field for the current line.</remarks>
        EndField,
    }
}