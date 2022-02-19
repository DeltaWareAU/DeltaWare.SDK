namespace DeltaWare.SDK.Core.Paging
{
    public class PagedQuery : IPagedQuery
    {
        public int PageIndex { get; set; }
        public int PageItems { get; set; }
        public string SearchString { get; set; }
        public bool SortDescending { get; set; }
        public string SortString { get; set; }
    }
}