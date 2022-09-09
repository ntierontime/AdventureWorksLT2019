using Framework.Models;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients
{
    public partial class CustomerApiClient : Framework.MauiX.WebApiClientBase
    {
        public CustomerApiClient(AdventureWorksLT2019.MauiXApp.WebApiClients.Common.WebApiConfig webApiConfig)
            : base(webApiConfig.WebApiRootUrl, "CustomerApi")
        {
        }

        public async Task<Framework.Models.ListResponse<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel[]>> Search(
            AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery query)
        {
            const string actionName = nameof(Search);
            string url = GetHttpRequestUrl(actionName);
            var response = await Post<AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery, Framework.Models.ListResponse<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel[]>>(url, query);
            return response;
        }

        public async Task<AdventureWorksLT2019.MauiXApp.DataModels.CustomerCompositeModel> GetCompositeModel(
            AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier id)
        {
            const string actionName = nameof(GetCompositeModel);
            string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
            var response = await Get<AdventureWorksLT2019.MauiXApp.DataModels.CustomerCompositeModel>(url);
            return response;
        }

        public async Task<Framework.Models.Response> BulkDelete(List<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier> ids)
        {
            const string actionName = nameof(BulkDelete);
            string url = GetHttpRequestUrl(actionName);
            var response = await Post<List<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier>, Framework.Models.Response>(url, ids);
            return response;
        }

        public async Task<Framework.Models.ListResponse<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel[]>> BulkUpdate(Framework.Models.BatchActionRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> data)
        {
            const string actionName = nameof(BulkUpdate);
            string url = GetHttpRequestUrl(actionName);
            var response = await Post<Framework.Models.BatchActionRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>, Framework.Models.ListResponse<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel[]>>(url, data);
            return response;
        }

        public async Task<Framework.Models.Response<Framework.Models.MultiItemsCUDRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>>> MultiItemsCUD(
            Framework.Models.MultiItemsCUDRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> input)
        {
            const string actionName = nameof(MultiItemsCUD);
            string url = GetHttpRequestUrl(actionName);
            var response = await Post<Framework.Models.MultiItemsCUDRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>, Framework.Models.Response<Framework.Models.MultiItemsCUDRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>>>(url, input);
            return response;
        }

        public async Task<Framework.Models.Response<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>> Update(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier id, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel input)
        {
            const string actionName = nameof(Update);
            string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
            var response = await Put<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, Framework.Models.Response<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>>(url, input);
            return response;
        }

        public async Task<Framework.Models.Response<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>> Get(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier id)
        {
            const string actionName = nameof(Get);
            string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
            var response = await Get<Framework.Models.Response<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>>(url);
            return response;
        }

        public async Task<Framework.Models.Response<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>> Create(AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel input)
        {
            const string actionName = nameof(Create);
            string url = GetHttpRequestUrl(actionName);
            var response = await Post<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, Framework.Models.Response<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>>(url, input);
            return response;
        }

        public async Task<Framework.Models.Response> Delete(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier id)
        {
            const string actionName = nameof(Get);
            string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
            var response = await Delete<Framework.Models.Response>(url);
            return response;
        }
    }
}
