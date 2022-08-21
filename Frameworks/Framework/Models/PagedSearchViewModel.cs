namespace Framework.Models
{
    public class PagedSearchViewModel<TQuery, TResponseBody>: PagedViewModel<TResponseBody>
        where TQuery : class
    {
        public TQuery Query { get; set; } = null!;
        public List<NameValuePair> PageSizeList { get; set; } = null!;
        public List<NameValuePair> OrderByList { get; set; } = null!;
        public List<NameValuePair> TextSearchTypeList { get; set; } = null!;
        public Dictionary<string, List<NameValuePair>> OtherDropDownLists { get; set; } = new Dictionary<string, List<NameValuePair>>();
    }
}

