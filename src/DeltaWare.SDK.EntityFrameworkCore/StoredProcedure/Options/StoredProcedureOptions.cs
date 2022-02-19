namespace DeltaWare.SDK.EntityFrameworkCore.StoredProcedure.Options
{
    internal class StoredProcedureOptions : IStoredProcedureOptionsBuilder, IStoredProcedureOptions
    {
        public int? Timeout { get; set; }
    }
}