namespace Framework.Models
{
    /// <summary>
    /// parameters between UI View and Controllers
    /// </summary>
    public class UIParams
    {
        public bool AdvancedQuery { get; set; } = false;
        public int IndexInArray { get; set; }
        public PagedViewOptions? PagedViewOption { get; set; } = PagedViewOptions.Table;
        public ViewItemTemplateNames? Template { get; set; } = ViewItemTemplateNames.Details;
    }
}

