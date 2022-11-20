namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.StoredProcedure.Options
{
    internal interface IStoredProcedureOptions
    {
        /// <summary>
        /// Specifies the timeout for the Store Procedure.
        /// </summary>
        int? Timeout { get; }
    }
}