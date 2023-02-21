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

        public UIItemFeatures GetBuildVersionUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
                ShowItemButtons = true,
                CanGotoDashboard = true,
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

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetErrorLogUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
                ShowItemButtons = true,
                CanGotoDashboard = true,
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
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetAddressUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
                ShowItemButtons = true,
                CanGotoDashboard = true,
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
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
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
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
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
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetCustomerAddressUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
                ShowItemButtons = true,
                CanGotoDashboard = true,
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

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
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
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductCategoryUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
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

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductDescriptionUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
                ShowItemButtons = true,
                CanGotoDashboard = true,
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

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductModelUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
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

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductModelProductDescriptionUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
                ShowItemButtons = true,
                CanGotoDashboard = true,
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

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetSalesOrderDetailUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
                ShowItemButtons = true,
                CanGotoDashboard = true,
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

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
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
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
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
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table,
                        ListViewOptions.Tiles,
                        ListViewOptions.SlideShow,
                        ListViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }
    }
}

