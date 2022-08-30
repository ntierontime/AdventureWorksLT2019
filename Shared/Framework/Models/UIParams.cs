using Framework.Common;
namespace Framework.Models
{
    /// <summary>
    /// parameters between UI View and Controllers
    /// </summary>
    public class UIParams
    {
        public bool AdvancedQuery { get; set; } = false;
        public int IndexInArray { get; set; }
        public ListViewOptions? PagedViewOption { get; set; } = ListViewOptions.Table;
        public string Template { get; set; } = ViewItemTemplates.Details.ToString();
    }
}

