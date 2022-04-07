namespace DeltaWare.SDK.Data.Paging
{
    public interface IPagedResult<out TResult>
    {
        public TResult[] Data { get; }
        public int FilteredRecords { get; }
        public int TotalRecords { get; }
    }
}