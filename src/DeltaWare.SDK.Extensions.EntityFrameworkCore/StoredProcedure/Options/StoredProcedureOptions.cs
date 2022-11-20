namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.StoredProcedure.Options
{
    internal class StoredProcedureOptions : IStoredProcedureOptionsBuilder, IStoredProcedureOptions
    {
        public int? Timeout { get; set; }
    }
}