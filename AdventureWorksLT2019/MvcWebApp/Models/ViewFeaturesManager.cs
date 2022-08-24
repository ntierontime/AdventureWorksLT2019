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

        public void DefaultUIParamsIfNeeds(UIParams uiParams, PagedViewOptions defaultPagedViewOption)
        {
            if (!uiParams.PagedViewOption.HasValue)
            {
                uiParams.PagedViewOption = defaultPagedViewOption;
            }
            if (uiParams.PagedViewOption == PagedViewOptions.EditableTable)
            {
                uiParams.Template = ViewItemTemplateNames.Edit;
            }
            else if(!uiParams.Template.HasValue)
            {
                uiParams.Template = ViewItemTemplateNames.Delete;
            }
        }

        public PaginationOptions HardCodePaginationOption(PagedViewOptions pagedViewOption, PaginationOptions original)
        {
            if (original == PaginationOptions.NoPagination)
                return PaginationOptions.NoPagination;
            else if (pagedViewOption == PagedViewOptions.Table || pagedViewOption == PagedViewOptions.EditableTable)
                return PaginationOptions.PageIndexesAndAllButtons;
            return original;
        }

        private static UIParams DefaultUIParams(PagedViewOptions defaultPagedViewOption)
        {
            return new UIParams
            {
                AdvancedQuery = false, //
                PagedViewOption = defaultPagedViewOption,
                Template = defaultPagedViewOption == PagedViewOptions.EditableTable
                        ? ViewItemTemplateNames.Edit
                        : ViewItemTemplateNames.Details,
            };
        }

        public UIItemFeatures GetBuildVersionUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.None,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.None,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetErrorLogUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetAddressUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetCustomerUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowItemButtons = true,
                CanGotoDashboard = false,
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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,
                    CanBulkActions = true,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetCustomerAddressUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.None,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.None,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowItemButtons = true,
                CanGotoDashboard = false,
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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductCategoryUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowItemButtons = true,
                CanGotoDashboard = false,
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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductDescriptionUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductModelUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowItemButtons = true,
                CanGotoDashboard = false,
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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetProductModelProductDescriptionUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.None,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.None,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetSalesOrderDetailUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.None,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.None,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetSalesOrderHeaderUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowItemButtons = true,
                CanGotoDashboard = false,
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

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = true,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }
    }
}

