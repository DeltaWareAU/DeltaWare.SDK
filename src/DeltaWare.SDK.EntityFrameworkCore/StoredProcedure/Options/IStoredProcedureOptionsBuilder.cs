namespace DeltaWare.SDK.EntityFrameworkCore.StoredProcedure.Options
{
    public interface IStoredProcedureOptionsBuilder
    {
        /// <summary>
        /// Specifies the timeout for the Store Procedure.
        /// </summary>
        int? Timeout { get; set; }
    }
}