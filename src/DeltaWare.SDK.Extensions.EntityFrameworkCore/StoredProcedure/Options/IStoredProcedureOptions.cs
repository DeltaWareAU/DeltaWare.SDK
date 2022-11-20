namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.StoredProcedure.Options
{
    internal interface IStoredProcedureOptions
    {
        /// <summary>
        /// Specifies the timeout for the Store Procedure.
        /// </summary>
        int? Timeout { get; }
    }
}