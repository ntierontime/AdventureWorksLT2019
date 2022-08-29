using AdventureWorksLT2019.ServiceContracts;
using Framework.Models;
using Framework.Mvc.Models;
using AdventureWorksLT2019.Models;
using AdventureWorksLT2019.Resx;
namespace AdventureWorksLT2019.MvcWebApp.Models
{
    public class DashboardViewModelHelper
    {
        private readonly IDropDownListService _dropDownListService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeaturesManager;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly ListSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<DashboardViewModelHelper> _logger;

        public DashboardViewModelHelper(
            IDropDownListService dropDownListService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeaturesManager,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            ListSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<DashboardViewModelHelper> logger)
        {
            _dropDownListService = dropDownListService;
            _selectListHelper = selectListHelper;
            _viewFeaturesManager = viewFeaturesManager;
            _mvcItemViewModelHelper = mvcItemViewModelHelper;
            _pagedSearchViewModelHelper = pagedSearchViewModelHelper;
            _localizor = localizor;
            _logger = logger;
        }

        public async Task<MvcItemViewModel<BuildVersionDataModel>> GetBuildVersionMvcItemViewModel(
            BuildVersionDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetBuildVersionMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<ListSearchViewModel<BuildVersionAdvancedQuery, BuildVersionDataModel[]>> GetBuildVersionPagedSearchViewModel(
            BuildVersionAdvancedQuery query,
            BuildVersionDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetBuildVersionPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ErrorLogDataModel>> GetErrorLogMvcItemViewModel(
            ErrorLogDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetErrorLogMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<ListSearchViewModel<ErrorLogAdvancedQuery, ErrorLogDataModel[]>> GetErrorLogPagedSearchViewModel(
            ErrorLogAdvancedQuery query,
            ErrorLogDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetErrorLogPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<AddressDataModel>> GetAddressMvcItemViewModel(
            AddressDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetAddressMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<ListSearchViewModel<AddressAdvancedQuery, AddressDataModel[]>> GetAddressPagedSearchViewModel(
            AddressAdvancedQuery query,
            AddressDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetAddressPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<CustomerDataModel>> GetCustomerMvcItemViewModel(
            CustomerDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetCustomerMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<ListSearchViewModel<CustomerAdvancedQuery, CustomerDataModel[]>> GetCustomerPagedSearchViewModel(
            CustomerAdvancedQuery query,
            CustomerDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetCustomerPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<CustomerAddressDataModel.DefaultView>> GetCustomerAddressMvcItemViewModel(
            CustomerAddressDataModel.DefaultView responseBody,
            CompositeItemModel compositeItem,
            bool loadTopLevelDropDownListsFromDatabase = false,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase = null)
        {
            return await _mvcItemViewModelHelper.GetCustomerAddressMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody, loadTopLevelDropDownListsFromDatabase, topLevelDropDownListsFromDatabase);
        }
        public async Task<ListSearchViewModel<CustomerAddressAdvancedQuery, CustomerAddressDataModel.DefaultView[]>> GetCustomerAddressPagedSearchViewModel(
            CustomerAddressAdvancedQuery query,
            CustomerAddressDataModel.DefaultView[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetCustomerAddressPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ProductDataModel.DefaultView>> GetProductMvcItemViewModel(
            ProductDataModel.DefaultView responseBody,
            CompositeItemModel compositeItem,
            bool loadTopLevelDropDownListsFromDatabase = false,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase = null)
        {
            return await _mvcItemViewModelHelper.GetProductMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody, loadTopLevelDropDownListsFromDatabase, topLevelDropDownListsFromDatabase);
        }
        public async Task<ListSearchViewModel<ProductAdvancedQuery, ProductDataModel.DefaultView[]>> GetProductPagedSearchViewModel(
            ProductAdvancedQuery query,
            ProductDataModel.DefaultView[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetProductPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ProductCategoryDataModel.DefaultView>> GetProductCategoryMvcItemViewModel(
            ProductCategoryDataModel.DefaultView responseBody,
            CompositeItemModel compositeItem,
            bool loadTopLevelDropDownListsFromDatabase = false,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase = null)
        {
            return await _mvcItemViewModelHelper.GetProductCategoryMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody, loadTopLevelDropDownListsFromDatabase, topLevelDropDownListsFromDatabase);
        }
        public async Task<ListSearchViewModel<ProductCategoryAdvancedQuery, ProductCategoryDataModel.DefaultView[]>> GetProductCategoryPagedSearchViewModel(
            ProductCategoryAdvancedQuery query,
            ProductCategoryDataModel.DefaultView[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetProductCategoryPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ProductDescriptionDataModel>> GetProductDescriptionMvcItemViewModel(
            ProductDescriptionDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetProductDescriptionMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<ListSearchViewModel<ProductDescriptionAdvancedQuery, ProductDescriptionDataModel[]>> GetProductDescriptionPagedSearchViewModel(
            ProductDescriptionAdvancedQuery query,
            ProductDescriptionDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetProductDescriptionPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ProductModelDataModel>> GetProductModelMvcItemViewModel(
            ProductModelDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetProductModelMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<ListSearchViewModel<ProductModelAdvancedQuery, ProductModelDataModel[]>> GetProductModelPagedSearchViewModel(
            ProductModelAdvancedQuery query,
            ProductModelDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetProductModelPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ProductModelProductDescriptionDataModel.DefaultView>> GetProductModelProductDescriptionMvcItemViewModel(
            ProductModelProductDescriptionDataModel.DefaultView responseBody,
            CompositeItemModel compositeItem,
            bool loadTopLevelDropDownListsFromDatabase = false,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase = null)
        {
            return await _mvcItemViewModelHelper.GetProductModelProductDescriptionMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody, loadTopLevelDropDownListsFromDatabase, topLevelDropDownListsFromDatabase);
        }
        public async Task<ListSearchViewModel<ProductModelProductDescriptionAdvancedQuery, ProductModelProductDescriptionDataModel.DefaultView[]>> GetProductModelProductDescriptionPagedSearchViewModel(
            ProductModelProductDescriptionAdvancedQuery query,
            ProductModelProductDescriptionDataModel.DefaultView[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetProductModelProductDescriptionPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>> GetSalesOrderDetailMvcItemViewModel(
            SalesOrderDetailDataModel.DefaultView responseBody,
            CompositeItemModel compositeItem,
            bool loadTopLevelDropDownListsFromDatabase = false,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase = null)
        {
            return await _mvcItemViewModelHelper.GetSalesOrderDetailMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody, loadTopLevelDropDownListsFromDatabase, topLevelDropDownListsFromDatabase);
        }
        public async Task<ListSearchViewModel<SalesOrderDetailAdvancedQuery, SalesOrderDetailDataModel.DefaultView[]>> GetSalesOrderDetailPagedSearchViewModel(
            SalesOrderDetailAdvancedQuery query,
            SalesOrderDetailDataModel.DefaultView[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetSalesOrderDetailPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>> GetSalesOrderHeaderMvcItemViewModel(
            SalesOrderHeaderDataModel.DefaultView responseBody,
            CompositeItemModel compositeItem,
            bool loadTopLevelDropDownListsFromDatabase = false,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase = null)
        {
            return await _mvcItemViewModelHelper.GetSalesOrderHeaderMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody, loadTopLevelDropDownListsFromDatabase, topLevelDropDownListsFromDatabase);
        }
        public async Task<ListSearchViewModel<SalesOrderHeaderAdvancedQuery, SalesOrderHeaderDataModel.DefaultView[]>> GetSalesOrderHeaderPagedSearchViewModel(
            SalesOrderHeaderAdvancedQuery query,
            SalesOrderHeaderDataModel.DefaultView[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetSalesOrderHeaderPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }
    }
}

