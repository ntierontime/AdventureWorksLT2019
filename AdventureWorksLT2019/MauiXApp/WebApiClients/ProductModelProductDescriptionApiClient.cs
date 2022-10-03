using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class ProductModelProductDescriptionApiClient : WebApiClientBase
{
    public ProductModelProductDescriptionApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "ProductModelProductDescriptionApi")
    {
    }

    public async Task<ListResponse<ProductModelProductDescriptionDataModel[]>> Search(
        ProductModelProductDescriptionAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductModelProductDescriptionAdvancedQuery, ListResponse<ProductModelProductDescriptionDataModel[]>>(url, query);
        return response;
    }

    public async Task<ProductModelProductDescriptionCompositeModel> GetCompositeModel(
        ProductModelProductDescriptionIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<ProductModelProductDescriptionCompositeModel>(url);
        return response;
    }

    public async Task<Response> BulkDelete(List<ProductModelProductDescriptionIdentifier> ids)
    {
        const string actionName = nameof(BulkDelete);
        string url = GetHttpRequestUrl(actionName);
        var response = await Put<List<ProductModelProductDescriptionIdentifier>, Response>(url, ids);
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel> input)
    {
        const string actionName = nameof(MultiItemsCUD);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel>, Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel>>>(url, input);
        return response;
    }

    public async Task<Response<ProductModelProductDescriptionDataModel>> Update(ProductModelProductDescriptionIdentifier id, ProductModelProductDescriptionDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<ProductModelProductDescriptionDataModel, Response<ProductModelProductDescriptionDataModel>>(url, input);
        return response;
    }

    public async Task<Response<ProductModelProductDescriptionDataModel>> Get(ProductModelProductDescriptionIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<ProductModelProductDescriptionDataModel>>(url);
        return response;
    }

    public async Task<Response<ProductModelProductDescriptionDataModel>> Create(ProductModelProductDescriptionDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductModelProductDescriptionDataModel, Response<ProductModelProductDescriptionDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(ProductModelProductDescriptionIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

