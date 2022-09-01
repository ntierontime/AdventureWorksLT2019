using Framework.Models;

namespace AdventureWorksLT2019.MvcWebApp.Models
{
    public class UIAvailableFeaturesManager
    {
        public UIAvailableFeatures GetFullAvailableFeatures(UIParams? uiParams)
        {
            return new UIAvailableFeatures
            {
                AvailableUISearchOptions = new List<UISearchOptions> { UISearchOptions.TextSearch, UISearchOptions.RegularSearch, UISearchOptions.AdvancedSearch, UISearchOptions.DynamicSearch, UISearchOptions.SpecialSearchParameters },

                HasCreateView = true,
                HasDeleteView = true,
                HasDetailsView = true,
                HasEditView = true,
                HasDashboard = true,
                HasSearchView = true,

                HasBulkDelete = true,
                BulkActions = null,
                AvailableListViewFeatures = new Dictionary<ListViewOptions, ViewItemTemplates[]>
                {
                    { ListViewOptions.Table, new ViewItemTemplates[] { ViewItemTemplates.Create, ViewItemTemplates.Delete, ViewItemTemplates.Details, ViewItemTemplates.Edit } },
                    { ListViewOptions.Tiles, new ViewItemTemplates[] { ViewItemTemplates.Create, ViewItemTemplates.Delete, ViewItemTemplates.Details, ViewItemTemplates.Edit } },
                    { ListViewOptions.SlideShow, new ViewItemTemplates[] { ViewItemTemplates.Details } },
                    { ListViewOptions.EditableTable, new ViewItemTemplates[] { ViewItemTemplates.Create, ViewItemTemplates.Edit } },
                },
            };
        }
    }
}

