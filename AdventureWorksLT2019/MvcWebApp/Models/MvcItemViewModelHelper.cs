using AdventureWorksLT2019.ServiceContracts;
using Framework.Models;
using AdventureWorksLT2019.Resx;
using AdventureWorksLT2019.Models;

namespace AdventureWorksLT2019.MvcWebApp.Models
{
    public class MvcItemViewModelHelper
    {
        private readonly IDropDownListService _dropDownListService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeaturesManager;
        private readonly IUIStrings _localizor;
        private readonly ILogger<MvcItemViewModelHelper> _logger;

        public MvcItemViewModelHelper(
            IDropDownListService dropDownListService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeaturesManager,
            IUIStrings localizor,
            ILogger<MvcItemViewModelHelper> logger)
        {
            _dropDownListService = dropDownListService;
            _selectListHelper = selectListHelper;
            _viewFeaturesManager = viewFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }

        public async Task<MvcItemViewModel<BuildVersionDataModel>> GetBuildVersionMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            BuildVersionDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<BuildVersionDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetBuildVersionUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ErrorLogDataModel>> GetErrorLogMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ErrorLogDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ErrorLogDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetErrorLogUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<AddressDataModel>> GetAddressMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            AddressDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<AddressDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetAddressUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<CustomerDataModel>> GetCustomerMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            CustomerDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<CustomerDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetCustomerUIItemFeatures(uiParams.PagedViewOption ?? PagedViewOptions.Table),
            });

            return result;
        }

        public async Task<MvcItemViewModel<CustomerAddressDataModel.DefaultView>> GetCustomerAddressMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            CustomerAddressDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<CustomerAddressDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetCustomerAddressUIItemFeatures(),
            });

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetCustomerAddressTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<MvcItemViewModel<ProductDataModel.DefaultView>> GetProductMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ProductDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ProductDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetProductUIItemFeatures(),
            });

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<MvcItemViewModel<ProductCategoryDataModel.DefaultView>> GetProductCategoryMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ProductCategoryDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ProductCategoryDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetProductCategoryUIItemFeatures(),
            });

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductCategoryTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<MvcItemViewModel<ProductDescriptionDataModel>> GetProductDescriptionMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ProductDescriptionDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ProductDescriptionDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetProductDescriptionUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ProductModelDataModel>> GetProductModelMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ProductModelDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ProductModelDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetProductModelUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ProductModelProductDescriptionDataModel.DefaultView>> GetProductModelProductDescriptionMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ProductModelProductDescriptionDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ProductModelProductDescriptionDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetProductModelProductDescriptionUIItemFeatures(),
            });

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetProductModelProductDescriptionTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>> GetSalesOrderDetailMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            SalesOrderDetailDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetSalesOrderDetailUIItemFeatures(),
            });

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderDetailTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>> GetSalesOrderHeaderMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            SalesOrderHeaderDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetSalesOrderHeaderUIItemFeatures(uiParams.PagedViewOption ?? PagedViewOptions.Table),
            });

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

    }
}

