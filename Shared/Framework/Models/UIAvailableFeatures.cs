namespace Framework.Models
{
    public class UIAvailableFeatures
    {
        // public string EntityName { get; set; } = null!;
        // public bool Enabled { get; set; } = true;

        public List<UISearchOptions>? AvailableUISearchOptions { get; set; }

        public bool HasCreateView { get; set; } = false;
        public bool HasDeleteView { get; set; } = false;
        public bool HasDetailsView { get; set; } = false;
        public bool HasEditView { get; set; } = false;
        public bool HasDashboard { get; set; } = false;
        public bool HasSearchView { get; set; } = false;

        public bool HasBulkDelete { get; set; } = false;

        public List<string>? BulkActions { get; set; }
        public bool HasBulkActions { get { return BulkActions != null && BulkActions.Count > 0; } }

        public Dictionary<ListViewOptions, ViewItemTemplates[]>? AvailableListViewFeatures { get; set; }
        public bool HasListViews { get { return AvailableListViewFeatures != null && AvailableListViewFeatures.Count > 0; } }
    }
}

