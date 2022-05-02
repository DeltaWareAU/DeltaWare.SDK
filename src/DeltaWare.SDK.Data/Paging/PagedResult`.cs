namespace DeltaWare.SDK.Data.Paging
{
    public class PagedResult<TResult> : IPagedResult<TResult>
    {
        public TResult[] Data { get; }
        public int FilteredRecords { get; }
        public int TotalRecords { get; }

        public PagedResult(TResult[] data, int total, int filtered)
        {
            Data = data;
            TotalRecords = total;
            FilteredRecords = filtered;
        }
    }
}