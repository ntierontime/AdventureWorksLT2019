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
            Response response,
            BuildVersionDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<BuildVersionDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetBuildVersionUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ErrorLogDataModel>> GetErrorLogMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ErrorLogDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ErrorLogDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetErrorLogUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<AddressDataModel>> GetAddressMvcItemViewModel(
            UIParams uiParams,
            Response response,
            AddressDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<AddressDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetAddressUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<CustomerDataModel>> GetCustomerMvcItemViewModel(
            UIParams uiParams,
            Response response,
            CustomerDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<CustomerDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetCustomerUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<CustomerAddressDataModel.DefaultView>> GetCustomerAddressMvcItemViewModel(
            UIParams uiParams,
            Response response,
            CustomerAddressDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<CustomerAddressDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
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
            Response response,
            ProductDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ProductDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
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
            Response response,
            ProductCategoryDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ProductCategoryDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
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
            Response response,
            ProductDescriptionDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ProductDescriptionDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetProductDescriptionUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ProductModelDataModel>> GetProductModelMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ProductModelDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ProductModelDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetProductModelUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ProductModelProductDescriptionDataModel.DefaultView>> GetProductModelProductDescriptionMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ProductModelProductDescriptionDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ProductModelProductDescriptionDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
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
            Response response,
            SalesOrderDetailDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<SalesOrderDetailDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
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
            Response response,
            SalesOrderHeaderDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<SalesOrderHeaderDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetSalesOrderHeaderUIItemFeatures(),
            });

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

    }
}

