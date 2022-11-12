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
}

