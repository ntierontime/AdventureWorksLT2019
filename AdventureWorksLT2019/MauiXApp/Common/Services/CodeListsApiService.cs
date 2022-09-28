using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;

namespace AdventureWorksLT2019.MauiXApp.Common.Services;

public partial class CodeListsApiService
{
    private readonly CodeListsApiClient _codeListsApiClient;
    public CodeListsApiService(CodeListsApiClient codeListsApiClient)
    {
        _codeListsApiClient = codeListsApiClient;
    }

    public async Task<ListResponse<NameValuePair<byte>[]>> GetBuildVersionCodeList(
        BuildVersionAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetBuildVersionCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetErrorLogCodeList(
        ErrorLogAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetErrorLogCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetAddressCodeList(
        AddressAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetAddressCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetCustomerCodeList(
        CustomerAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetCustomerCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetCustomerAddressCodeList(
        CustomerAddressAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetCustomerAddressCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetProductCodeList(
        ProductAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetProductCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetProductCategoryCodeList(
        ProductCategoryAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetProductCategoryCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetProductDescriptionCodeList(
        ProductDescriptionAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetProductDescriptionCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetProductModelCodeList(
        ProductModelAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetProductModelCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetProductModelProductDescriptionCodeList(
        ProductModelProductDescriptionAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetProductModelProductDescriptionCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetSalesOrderDetailCodeList(
        SalesOrderDetailAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetSalesOrderDetailCodeList(query);
        return response;
    }

    public async Task<ListResponse<NameValuePair<int>[]>> GetSalesOrderHeaderCodeList(
        SalesOrderHeaderAdvancedQuery query)
    {
        var response = await _codeListsApiClient.GetSalesOrderHeaderCodeList(query);
        return response;
    }

}

