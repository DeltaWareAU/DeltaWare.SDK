namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.StoredProcedure.Options
{
    public interface IStoredProcedureOptionsBuilder
    {
        /// <summary>
        /// Specifies the timeout for the Store Procedure.
        /// </summary>
        int? Timeout { get; set; }
    }
}