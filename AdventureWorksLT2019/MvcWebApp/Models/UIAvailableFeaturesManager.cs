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
                AvailableListViewFeatures = new Dictionary<PagedViewOptions, ViewItemTemplateNames[]>
                {
                    { PagedViewOptions.Table, new ViewItemTemplateNames[] { ViewItemTemplateNames.Create, ViewItemTemplateNames.Delete, ViewItemTemplateNames.Details, ViewItemTemplateNames.Edit } },
                    { PagedViewOptions.Tiles, new ViewItemTemplateNames[] { ViewItemTemplateNames.Create, ViewItemTemplateNames.Delete, ViewItemTemplateNames.Details, ViewItemTemplateNames.Edit } },
                    { PagedViewOptions.SlideShow, new ViewItemTemplateNames[] { ViewItemTemplateNames.Details } },
                    { PagedViewOptions.EditableTable, new ViewItemTemplateNames[] { ViewItemTemplateNames.Create, ViewItemTemplateNames.Edit } },
                },
            };
        }
    }
}

