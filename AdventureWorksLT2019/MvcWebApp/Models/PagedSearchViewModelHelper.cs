using AdventureWorksLT2019.ServiceContracts;
using Framework.Models;
using AdventureWorksLT2019.Models;
using AdventureWorksLT2019.Resx;
namespace AdventureWorksLT2019.MvcWebApp.Models
{
    public class PagedSearchViewModelHelper
    {
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeaturesManager;
        private readonly IUIStrings _localizor;
        private readonly ILogger<PagedSearchViewModelHelper> _logger;

        public PagedSearchViewModelHelper(
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeaturesManager,
            IUIStrings localizor,
            ILogger<PagedSearchViewModelHelper> logger)
        {
            _dropDownListService = dropDownListService;
            _orderBysListHelper = orderBysListHelper;
            _selectListHelper = selectListHelper;
            _viewFeaturesManager = viewFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }

        public async Task<PagedSearchViewModel<BuildVersionAdvancedQuery, BuildVersionDataModel[]>> GetBuildVersionPagedSearchViewModel(
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
                new PagedResponse<BuildVersionDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<BuildVersionAdvancedQuery, BuildVersionDataModel[]>> GetBuildVersionPagedSearchViewModel(
            string key,
            UIParams uiParams,
            BuildVersionAdvancedQuery query,
            PagedResponse<BuildVersionDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<BuildVersionAdvancedQuery, BuildVersionDataModel[]>
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

        public async Task<PagedSearchViewModel<ErrorLogAdvancedQuery, ErrorLogDataModel[]>> GetErrorLogPagedSearchViewModel(
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
                new PagedResponse<ErrorLogDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ErrorLogAdvancedQuery, ErrorLogDataModel[]>> GetErrorLogPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ErrorLogAdvancedQuery query,
            PagedResponse<ErrorLogDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ErrorLogAdvancedQuery, ErrorLogDataModel[]>
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

        public async Task<PagedSearchViewModel<AddressAdvancedQuery, AddressDataModel[]>> GetAddressPagedSearchViewModel(
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
                new PagedResponse<AddressDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<AddressAdvancedQuery, AddressDataModel[]>> GetAddressPagedSearchViewModel(
            string key,
            UIParams uiParams,
            AddressAdvancedQuery query,
            PagedResponse<AddressDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<AddressAdvancedQuery, AddressDataModel[]>
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

        public async Task<PagedSearchViewModel<CustomerAdvancedQuery, CustomerDataModel[]>> GetCustomerPagedSearchViewModel(
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
                new PagedResponse<CustomerDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<CustomerAdvancedQuery, CustomerDataModel[]>> GetCustomerPagedSearchViewModel(
            string key,
            UIParams uiParams,
            CustomerAdvancedQuery query,
            PagedResponse<CustomerDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<CustomerAdvancedQuery, CustomerDataModel[]>
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

        public async Task<PagedSearchViewModel<CustomerAddressAdvancedQuery, CustomerAddressDataModel.DefaultView[]>> GetCustomerAddressPagedSearchViewModel(
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
                new PagedResponse<CustomerAddressDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<CustomerAddressAdvancedQuery, CustomerAddressDataModel.DefaultView[]>> GetCustomerAddressPagedSearchViewModel(
            string key,
            UIParams uiParams,
            CustomerAddressAdvancedQuery query,
            PagedResponse<CustomerAddressDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<CustomerAddressAdvancedQuery, CustomerAddressDataModel.DefaultView[]>
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

        public async Task<PagedSearchViewModel<ProductAdvancedQuery, ProductDataModel.DefaultView[]>> GetProductPagedSearchViewModel(
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
                new PagedResponse<ProductDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ProductAdvancedQuery, ProductDataModel.DefaultView[]>> GetProductPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductAdvancedQuery query,
            PagedResponse<ProductDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ProductAdvancedQuery, ProductDataModel.DefaultView[]>
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

        public async Task<PagedSearchViewModel<ProductCategoryAdvancedQuery, ProductCategoryDataModel.DefaultView[]>> GetProductCategoryPagedSearchViewModel(
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
                new PagedResponse<ProductCategoryDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ProductCategoryAdvancedQuery, ProductCategoryDataModel.DefaultView[]>> GetProductCategoryPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductCategoryAdvancedQuery query,
            PagedResponse<ProductCategoryDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ProductCategoryAdvancedQuery, ProductCategoryDataModel.DefaultView[]>
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

        public async Task<PagedSearchViewModel<ProductDescriptionAdvancedQuery, ProductDescriptionDataModel[]>> GetProductDescriptionPagedSearchViewModel(
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
                new PagedResponse<ProductDescriptionDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ProductDescriptionAdvancedQuery, ProductDescriptionDataModel[]>> GetProductDescriptionPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductDescriptionAdvancedQuery query,
            PagedResponse<ProductDescriptionDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ProductDescriptionAdvancedQuery, ProductDescriptionDataModel[]>
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

        public async Task<PagedSearchViewModel<ProductModelAdvancedQuery, ProductModelDataModel[]>> GetProductModelPagedSearchViewModel(
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
                new PagedResponse<ProductModelDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ProductModelAdvancedQuery, ProductModelDataModel[]>> GetProductModelPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductModelAdvancedQuery query,
            PagedResponse<ProductModelDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ProductModelAdvancedQuery, ProductModelDataModel[]>
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

        public async Task<PagedSearchViewModel<ProductModelProductDescriptionAdvancedQuery, ProductModelProductDescriptionDataModel.DefaultView[]>> GetProductModelProductDescriptionPagedSearchViewModel(
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
                new PagedResponse<ProductModelProductDescriptionDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ProductModelProductDescriptionAdvancedQuery, ProductModelProductDescriptionDataModel.DefaultView[]>> GetProductModelProductDescriptionPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ProductModelProductDescriptionAdvancedQuery query,
            PagedResponse<ProductModelProductDescriptionDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ProductModelProductDescriptionAdvancedQuery, ProductModelProductDescriptionDataModel.DefaultView[]>
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

        public async Task<PagedSearchViewModel<SalesOrderDetailAdvancedQuery, SalesOrderDetailDataModel.DefaultView[]>> GetSalesOrderDetailPagedSearchViewModel(
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
                new PagedResponse<SalesOrderDetailDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<SalesOrderDetailAdvancedQuery, SalesOrderDetailDataModel.DefaultView[]>> GetSalesOrderDetailPagedSearchViewModel(
            string key,
            UIParams uiParams,
            SalesOrderDetailAdvancedQuery query,
            PagedResponse<SalesOrderDetailDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<SalesOrderDetailAdvancedQuery, SalesOrderDetailDataModel.DefaultView[]>
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

        public async Task<PagedSearchViewModel<SalesOrderHeaderAdvancedQuery, SalesOrderHeaderDataModel.DefaultView[]>> GetSalesOrderHeaderPagedSearchViewModel(
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
                new PagedResponse<SalesOrderHeaderDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<SalesOrderHeaderAdvancedQuery, SalesOrderHeaderDataModel.DefaultView[]>> GetSalesOrderHeaderPagedSearchViewModel(
            string key,
            UIParams uiParams,
            SalesOrderHeaderAdvancedQuery query,
            PagedResponse<SalesOrderHeaderDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<SalesOrderHeaderAdvancedQuery, SalesOrderHeaderDataModel.DefaultView[]>
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

