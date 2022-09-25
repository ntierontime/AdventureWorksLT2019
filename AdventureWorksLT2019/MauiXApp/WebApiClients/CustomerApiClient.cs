using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class CustomerApiClient : WebApiClientBase
{
    public CustomerApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "CustomerApi")
    {
    }

    public async Task<ListResponse<CustomerDataModel[]>> Search(
        CustomerAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<CustomerAdvancedQuery, ListResponse<CustomerDataModel[]>>(url, query);
        return response;
    }

    public async Task<CustomerCompositeModel> GetCompositeModel(
        CustomerIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<CustomerCompositeModel>(url);
        return response;
    }

    public async Task<ListResponse<CustomerDataModel[]>> BulkUpdate(BatchActionRequest<CustomerIdentifier, CustomerDataModel> data)
    {
        const string actionName = nameof(BulkUpdate);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<BatchActionRequest<CustomerIdentifier, CustomerDataModel>, ListResponse<CustomerDataModel[]>>(url, data);
        return response;
    }

    public async Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<CustomerDataModel, Response<CustomerDataModel>>(url, input);
        return response;
    }

    public async Task<Response<CustomerDataModel>> Get(CustomerIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<CustomerDataModel>>(url);
        return response;
    }

    public async Task<Response<CustomerDataModel>> Create(CustomerDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<CustomerDataModel, Response<CustomerDataModel>>(url, input);
        return response;
    }
}

