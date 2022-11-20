namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.StoredProcedure.Options
{
    internal class StoredProcedureOptions : IStoredProcedureOptionsBuilder, IStoredProcedureOptions
    {
        public int? Timeout { get; set; }
    }
}