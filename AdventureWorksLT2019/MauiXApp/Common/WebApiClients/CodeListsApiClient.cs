using AdventureWorksLT2019.MauiXApp.DataModels;
using Framework.MauiX;
using Framework.Models;

namespace AdventureWorksLT2019.MauiXApp.Common.WebApiClients;

public partial class CodeListsApiClient : WebApiClientBase
{
    public CodeListsApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "CodeListsApi")
    {
    }

    public async Task<ListResponse<NameValuePair<byte>[]>> GetBuildVersionCodeList(
        BuildVersionAdvancedQuery query)
    {
        const string actionName = nameof(GetBuildVersionCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<BuildVersionAdvancedQuery, ListResponse<NameValuePair<byte>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetErrorLogCodeList(
        ErrorLogAdvancedQuery query)
    {
        const string actionName = nameof(GetErrorLogCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ErrorLogAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetAddressCodeList(
        AddressAdvancedQuery query)
    {
        const string actionName = nameof(GetAddressCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<AddressAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetCustomerCodeList(
        CustomerAdvancedQuery query)
    {
        const string actionName = nameof(GetCustomerCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<CustomerAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetCustomerAddressCodeList(
        CustomerAddressAdvancedQuery query)
    {
        const string actionName = nameof(GetCustomerAddressCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<CustomerAddressAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetProductCodeList(
        ProductAdvancedQuery query)
    {
        const string actionName = nameof(GetProductCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetProductCategoryCodeList(
        ProductCategoryAdvancedQuery query)
    {
        const string actionName = nameof(GetProductCategoryCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductCategoryAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetProductDescriptionCodeList(
        ProductDescriptionAdvancedQuery query)
    {
        const string actionName = nameof(GetProductDescriptionCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductDescriptionAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetProductModelCodeList(
        ProductModelAdvancedQuery query)
    {
        const string actionName = nameof(GetProductModelCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductModelAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetProductModelProductDescriptionCodeList(
        ProductModelProductDescriptionAdvancedQuery query)
    {
        const string actionName = nameof(GetProductModelProductDescriptionCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductModelProductDescriptionAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetSalesOrderDetailCodeList(
        SalesOrderDetailAdvancedQuery query)
    {
        const string actionName = nameof(GetSalesOrderDetailCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<SalesOrderDetailAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetSalesOrderHeaderCodeList(
        SalesOrderHeaderAdvancedQuery query)
    {
        const string actionName = nameof(GetSalesOrderHeaderCodeList);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<SalesOrderHeaderAdvancedQuery, ListResponse<NameValuePair<int>[]>>(url, query);
        return response;
    }

}

