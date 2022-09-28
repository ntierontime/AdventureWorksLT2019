using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class CustomerAddressApiClient : WebApiClientBase
{
    public CustomerAddressApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "CustomerAddressApi")
    {
    }

    public async Task<ListResponse<CustomerAddressDataModel[]>> Search(
        CustomerAddressAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<CustomerAddressAdvancedQuery, ListResponse<CustomerAddressDataModel[]>>(url, query);
        return response;
    }

    public async Task<CustomerAddressCompositeModel> GetCompositeModel(
        CustomerAddressIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<CustomerAddressCompositeModel>(url);
        return response;
    }

    public async Task<Response> BulkDelete(List<CustomerAddressIdentifier> ids)
    {
        const string actionName = nameof(BulkDelete);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<List<CustomerAddressIdentifier>, Response>(url, ids);
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel> input)
    {
        const string actionName = nameof(MultiItemsCUD);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel>, Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel>>>(url, input);
        return response;
    }

    public async Task<Response<CustomerAddressDataModel>> Update(CustomerAddressIdentifier id, CustomerAddressDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<CustomerAddressDataModel, Response<CustomerAddressDataModel>>(url, input);
        return response;
    }

    public async Task<Response<CustomerAddressDataModel>> Get(CustomerAddressIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<CustomerAddressDataModel>>(url);
        return response;
    }

    public async Task<Response<CustomerAddressDataModel>> Create(CustomerAddressDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<CustomerAddressDataModel, Response<CustomerAddressDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(CustomerAddressIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

