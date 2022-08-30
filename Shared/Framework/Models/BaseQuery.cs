using Framework.Common;
namespace Framework.Models
{
    public class BaseQuery
    {
        public int PageSize { get; set; } = 10; // default 10 items per pages
        public int PageIndex { get; set; } = 1; // start from 1
        public string? OrderBys { get; set; }

        // public bool AdvancedQuery { get; set; } = false;
        // public ListViewOptions PagedViewOption { get; set; } = ListViewOptions.Table;
        public PaginationOptions PaginationOption { get; set; } = PaginationOptions.PageIndexesAndAllButtons;
        // public ViewItemTemplates Template { get; set; } = ViewItemTemplates.Details;
    }
}

