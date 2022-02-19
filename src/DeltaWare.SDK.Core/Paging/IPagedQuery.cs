namespace DeltaWare.SDK.Core.Paging
{
    public interface IPagedQuery
    {
        public int PageIndex { get; }
        public int PageItems { get; }
        public string SearchString { get; }
        public bool SortDescending { get; }
        public string SortString { get; }
    }
}