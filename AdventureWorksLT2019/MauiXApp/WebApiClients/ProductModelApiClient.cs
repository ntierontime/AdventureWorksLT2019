using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class ProductModelApiClient : WebApiClientBase
{
    public ProductModelApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "ProductModelApi")
    {
    }

    public async Task<ListResponse<ProductModelDataModel[]>> Search(
        ProductModelAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductModelAdvancedQuery, ListResponse<ProductModelDataModel[]>>(url, query);
        return response;
    }

    public async Task<ProductModelCompositeModel> GetCompositeModel(
        ProductModelIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<ProductModelCompositeModel>(url);
        return response;
    }

    public async Task<Response<ProductModelDataModel>> Update(ProductModelIdentifier id, ProductModelDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<ProductModelDataModel, Response<ProductModelDataModel>>(url, input);
        return response;
    }

    public async Task<Response<ProductModelDataModel>> Get(ProductModelIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<ProductModelDataModel>>(url);
        return response;
    }

    public async Task<Response<ProductModelDataModel>> Create(ProductModelDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductModelDataModel, Response<ProductModelDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(ProductModelIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

