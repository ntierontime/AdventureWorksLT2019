using AdventureWorksLT2019.ServiceContracts;
using Framework.Mvc.Models;
using Framework.Models;
using AdventureWorksLT2019.Models;
using AdventureWorksLT2019.Resx;
namespace AdventureWorksLT2019.MvcWebApp.Models
{
    public class ListSearchViewModelHelper
    {
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeaturesManager;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ListSearchViewModelHelper> _logger;

        public ListSearchViewModelHelper(
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeaturesManager,
            IUIStrings localizor,
            ILogger<ListSearchViewModelHelper> logger)
        {
            _dropDownListService = dropDownListService;
            _orderBysListHelper = orderBysListHelper;
            _selectListHelper = selectListHelper;
            _viewFeaturesManager = viewFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }

        public async Task<ListSearchViewModel<BuildVersionAdvancedQuery, BuildVersionDataModel[]>> GetBuildVersionPagedSearchViewModel(
            string key,
            UIParams uiParams,
            BuildVersionAdvancedQuery query,
            Response<PaginationResponse> response,
            BuildVersionDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetBuildVersionPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<BuildVersionDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<BuildVersionAdvancedQuery, BuildVersionDataModel[]>> GetBuildVersionPagedSearchViewModel(
            string key,
            UIParams uiParams,
            BuildVersionAdvancedQuery query,
            ListResponse<BuildVersionDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<BuildVersionAdvancedQuery, BuildVersionDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetBuildVersionOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetBuildVersionUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(BuildVersionAdvancedQuery.VersionDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
            otherDropDownLists.Add(nameof(BuildVersionAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<ListSearchViewModel<ErrorLogAdvancedQuery, ErrorLogDataModel[]>> GetErrorLogPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ErrorLogAdvancedQuery query,
            Response<PaginationResponse> response,
            ErrorLogDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetErrorLogPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<ErrorLogDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<ErrorLogAdvancedQuery, ErrorLogDataModel[]>> GetErrorLogPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ErrorLogAdvancedQuery query,
            ListResponse<ErrorLogDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<ErrorLogAdvancedQuery, ErrorLogDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetErrorLogOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetErrorLogUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(ErrorLogAdvancedQuery.ErrorTimeRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<ListSearchViewModel<AddressAdvancedQuery, AddressDataModel[]>> GetAddressPagedSearchViewModel(
            string key,
            UIParams uiParams,
            AddressAdvancedQuery query,
            Response<PaginationResponse> response,
            AddressDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetAddressPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<AddressDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<AddressAdvancedQuery, AddressDataModel[]>> GetAddressPagedSearchViewModel(
            string key,
            UIParams uiParams,
            AddressAdvancedQuery query,
            ListResponse<AddressDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<AddressAdvancedQuery, AddressDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetAddressOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetAddressUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(AddressAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<ListSearchViewModel<CustomerAdvancedQuery, CustomerDataModel[]>> GetCustomerPagedSearchViewModel(
            string key,
            UIParams uiParams,
            CustomerAdvancedQuery query,
            Response<PaginationResponse> response,
            CustomerDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetCustomerPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<CustomerDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<CustomerAdvancedQuery, CustomerDataModel[]>> GetCustomerPagedSearchViewModel(
            string key,
            UIParams uiParams,
            CustomerAdvancedQuery query,
            ListResponse<CustomerDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<CustomerAdvancedQuery, CustomerDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetCustomerOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetCustomerUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(CustomerAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
            otherDropDownLists.Add(nameof(CustomerAdvancedQuery.NameStyle), _selectListHelper.GetDefaultTrueFalseBooleanSelectList());
                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<ListSearchViewModel<CustomerAddressAdvancedQuery, CustomerAddressDataModel.DefaultView[]>> GetCustomerAddressPagedSearchViewModel(
            string key,
            UIParams uiParams,
            CustomerAddressAdvancedQuery query,
            Response<PaginationResponse> response,
            CustomerAddressDataModel.DefaultView[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetCustomerAddressPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<CustomerAddressDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<CustomerAddressAdvancedQuery, CustomerAddressDataModel.DefaultView[]>> GetCustomerAddressPagedSearchViewModel(
            string key,
            UIParams uiParams,
            CustomerAddressAdvancedQuery query,
            ListResponse<CustomerAddressDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<CustomerAddressAdvancedQuery, CustomerAddressDataModel.DefaultView[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetCustomerAddressOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetCustomerAddressUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(CustomerAddressAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }
            if (loadTopLevelDropDownListsFromDatabase)
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetCustomerAddressTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<ListSearchViewModel<ProductAdvancedQuery, ProductDataModel.DefaultView[]>> GetProductPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductAdvancedQuery query,
            Response<PaginationResponse> response,
            ProductDataModel.DefaultView[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetProductPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<ProductDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<ProductAdvancedQuery, ProductDataModel.DefaultView[]>> GetProductPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductAdvancedQuery query,
            ListResponse<ProductDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<ProductAdvancedQuery, ProductDataModel.DefaultView[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetProductOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetProductUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(ProductAdvancedQuery.SellStartDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
            otherDropDownLists.Add(nameof(ProductAdvancedQuery.SellEndDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
            otherDropDownLists.Add(nameof(ProductAdvancedQuery.DiscontinuedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
            otherDropDownLists.Add(nameof(ProductAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }
            if (loadTopLevelDropDownListsFromDatabase)
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<ListSearchViewModel<ProductCategoryAdvancedQuery, ProductCategoryDataModel.DefaultView[]>> GetProductCategoryPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductCategoryAdvancedQuery query,
            Response<PaginationResponse> response,
            ProductCategoryDataModel.DefaultView[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetProductCategoryPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<ProductCategoryDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<ProductCategoryAdvancedQuery, ProductCategoryDataModel.DefaultView[]>> GetProductCategoryPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductCategoryAdvancedQuery query,
            ListResponse<ProductCategoryDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<ProductCategoryAdvancedQuery, ProductCategoryDataModel.DefaultView[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetProductCategoryOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetProductCategoryUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(ProductCategoryAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }
            if (loadTopLevelDropDownListsFromDatabase)
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductCategoryTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<ListSearchViewModel<ProductDescriptionAdvancedQuery, ProductDescriptionDataModel[]>> GetProductDescriptionPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductDescriptionAdvancedQuery query,
            Response<PaginationResponse> response,
            ProductDescriptionDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetProductDescriptionPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<ProductDescriptionDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<ProductDescriptionAdvancedQuery, ProductDescriptionDataModel[]>> GetProductDescriptionPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductDescriptionAdvancedQuery query,
            ListResponse<ProductDescriptionDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<ProductDescriptionAdvancedQuery, ProductDescriptionDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetProductDescriptionOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetProductDescriptionUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(ProductDescriptionAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<ListSearchViewModel<ProductModelAdvancedQuery, ProductModelDataModel[]>> GetProductModelPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductModelAdvancedQuery query,
            Response<PaginationResponse> response,
            ProductModelDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetProductModelPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<ProductModelDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<ProductModelAdvancedQuery, ProductModelDataModel[]>> GetProductModelPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductModelAdvancedQuery query,
            ListResponse<ProductModelDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<ProductModelAdvancedQuery, ProductModelDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetProductModelOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetProductModelUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(ProductModelAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<ListSearchViewModel<ProductModelProductDescriptionAdvancedQuery, ProductModelProductDescriptionDataModel.DefaultView[]>> GetProductModelProductDescriptionPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductModelProductDescriptionAdvancedQuery query,
            Response<PaginationResponse> response,
            ProductModelProductDescriptionDataModel.DefaultView[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetProductModelProductDescriptionPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<ProductModelProductDescriptionAdvancedQuery, ProductModelProductDescriptionDataModel.DefaultView[]>> GetProductModelProductDescriptionPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductModelProductDescriptionAdvancedQuery query,
            ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<ProductModelProductDescriptionAdvancedQuery, ProductModelProductDescriptionDataModel.DefaultView[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetProductModelProductDescriptionOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetProductModelProductDescriptionUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(ProductModelProductDescriptionAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }
            if (loadTopLevelDropDownListsFromDatabase)
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductModelProductDescriptionTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<ListSearchViewModel<SalesOrderDetailAdvancedQuery, SalesOrderDetailDataModel.DefaultView[]>> GetSalesOrderDetailPagedSearchViewModel(
            string key,
            UIParams uiParams,
            SalesOrderDetailAdvancedQuery query,
            Response<PaginationResponse> response,
            SalesOrderDetailDataModel.DefaultView[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetSalesOrderDetailPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<SalesOrderDetailDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<SalesOrderDetailAdvancedQuery, SalesOrderDetailDataModel.DefaultView[]>> GetSalesOrderDetailPagedSearchViewModel(
            string key,
            UIParams uiParams,
            SalesOrderDetailAdvancedQuery query,
            ListResponse<SalesOrderDetailDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<SalesOrderDetailAdvancedQuery, SalesOrderDetailDataModel.DefaultView[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetSalesOrderDetailOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetSalesOrderDetailUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(SalesOrderDetailAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }
            if (loadTopLevelDropDownListsFromDatabase)
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderDetailTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<ListSearchViewModel<SalesOrderHeaderAdvancedQuery, SalesOrderHeaderDataModel.DefaultView[]>> GetSalesOrderHeaderPagedSearchViewModel(
            string key,
            UIParams uiParams,
            SalesOrderHeaderAdvancedQuery query,
            Response<PaginationResponse> response,
            SalesOrderHeaderDataModel.DefaultView[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetSalesOrderHeaderPagedSearchViewModel(
                key,
                uiParams,
                query,
                new ListResponse<SalesOrderHeaderDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<ListSearchViewModel<SalesOrderHeaderAdvancedQuery, SalesOrderHeaderDataModel.DefaultView[]>> GetSalesOrderHeaderPagedSearchViewModel(
            string key,
            UIParams uiParams,
            SalesOrderHeaderAdvancedQuery query,
            ListResponse<SalesOrderHeaderDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new ListSearchViewModel<SalesOrderHeaderAdvancedQuery, SalesOrderHeaderDataModel.DefaultView[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetSalesOrderHeaderOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetSalesOrderHeaderUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(SalesOrderHeaderAdvancedQuery.OrderDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
            otherDropDownLists.Add(nameof(SalesOrderHeaderAdvancedQuery.DueDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
            otherDropDownLists.Add(nameof(SalesOrderHeaderAdvancedQuery.ShipDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
            otherDropDownLists.Add(nameof(SalesOrderHeaderAdvancedQuery.ModifiedDateRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
            otherDropDownLists.Add(nameof(SalesOrderHeaderAdvancedQuery.OnlineOrderFlag), _selectListHelper.GetDefaultTrueFalseBooleanSelectList());
                result.OtherDropDownLists = otherDropDownLists;
            }
            if (loadTopLevelDropDownListsFromDatabase)
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

    }
}

