namespace Framework.Models
{
    public class CompositeListItemRequest
    {
        public int PageSize { get; set; } = 10; // default 10 items per pages
        public string? OrderBys { get; set; }
        public PaginationOptions PaginationOption { get; set; } = PaginationOptions.PageIndexesAndAllButtons;
    }
}

