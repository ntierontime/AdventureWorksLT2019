using Framework.Models;
using AdventureWorksLT2019.Resx;

namespace AdventureWorksLT2019.MvcWebApp.Models
{
    public class ViewFeaturesManager
    {
        private readonly UIAvailableFeaturesManager _uiAvailableFeaturesManager;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ViewFeaturesManager> _logger;

        public ViewFeaturesManager(
            UIAvailableFeaturesManager uiAvailableFeaturesManager,
            IUIStrings localizor,
            ILogger<ViewFeaturesManager> logger)
        {
            _uiAvailableFeaturesManager = uiAvailableFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }

        public void DefaultUIParamsIfNeeds(UIParams uiParams, ListViewOptions defaultPagedViewOption)
        {
            if (!uiParams.PagedViewOption.HasValue)
            {
                uiParams.PagedViewOption = defaultPagedViewOption;
            }
            if (uiParams.PagedViewOption == ListViewOptions.EditableTable)
            {
                uiParams.Template = ViewItemTemplates.Edit.ToString();
            }
            else if(string.IsNullOrEmpty(uiParams.Template))
            {
                uiParams.Template = ViewItemTemplates.Delete.ToString();
            }
        }

        public PaginationOptions HardCodePaginationOption(ListViewOptions pagedViewOption, PaginationOptions original)
        {
            if (original == PaginationOptions.NoPagination)
                return PaginationOptions.NoPagination;
            else if (pagedViewOption == ListViewOptions.Table || pagedViewOption == ListViewOptions.EditableTable)
                return PaginationOptions.PageIndexesAndAllButtons;
            return original;
        }

        private static UIParams DefaultUIParams(ListViewOptions defaultPagedViewOption)
        {
            return new UIParams
            {
                AdvancedQuery = false, //
                PagedViewOption = defaultPagedViewOption,
                Template = defaultPagedViewOption == ListViewOptions.EditableTable
                        ? ViewItemTemplates.Edit.ToString()
                        : ViewItemTemplates.Details.ToString(),
            };
        }

        public UIItemFeatures GetBuildVersionUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.None,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.None,

                ShowItemButtons = true,
                CanGotoDashboard = false,
            };

            return result;
        }

        public UIListSettingModel GetBuildVersionUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.None,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.None,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetErrorLogUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowItemButtons = true,
                CanGotoDashboard = false,
            };

            return result;
        }

        public UIListSettingModel GetErrorLogUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetAddressUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowItemButtons = true,
                CanGotoDashboard = false,
            };

            return result;
        }

        public UIListSettingModel GetAddressUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetCustomerUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
                ShowItemButtons = true,
                CanGotoDashboard = true,
            };

            return result;
        }

        public UIListSettingModel GetCustomerUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetCustomerAddressUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.None,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.None,

                ShowItemButtons = true,
                CanGotoDashboard = false,
            };

            return result;
        }

        public UIListSettingModel GetCustomerAddressUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.None,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.None,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowItemButtons = true,
                CanGotoDashboard = true,
            };

            return result;
        }

        public UIListSettingModel GetProductUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductCategoryUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Inline,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Inline,
                PrimayEditViewContainer = CrudViewContainers.Inline,

                ShowItemButtons = true,
                CanGotoDashboard = true,
            };

            return result;
        }

        public UIListSettingModel GetProductCategoryUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductDescriptionUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Inline,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Inline,
                PrimayEditViewContainer = CrudViewContainers.Inline,

                ShowItemButtons = true,
                CanGotoDashboard = false,
            };

            return result;
        }

        public UIListSettingModel GetProductDescriptionUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductModelUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Inline,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Inline,
                PrimayEditViewContainer = CrudViewContainers.Inline,

                ShowItemButtons = true,
                CanGotoDashboard = true,
            };

            return result;
        }

        public UIListSettingModel GetProductModelUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductModelProductDescriptionUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.None,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.None,

                ShowItemButtons = true,
                CanGotoDashboard = false,
            };

            return result;
        }

        public UIListSettingModel GetProductModelProductDescriptionUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.None,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.None,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetSalesOrderDetailUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.None,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.None,

                ShowItemButtons = true,
                CanGotoDashboard = false,
            };

            return result;
        }

        public UIListSettingModel GetSalesOrderDetailUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.None,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.None,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetSalesOrderHeaderUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
                ShowItemButtons = true,
                CanGotoDashboard = true,
            };

            return result;
        }

        public UIListSettingModel GetSalesOrderHeaderUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }
    }
}

