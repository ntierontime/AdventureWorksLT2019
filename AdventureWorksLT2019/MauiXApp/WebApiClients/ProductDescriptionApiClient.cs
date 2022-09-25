using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class ProductDescriptionApiClient : WebApiClientBase
{
    public ProductDescriptionApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "ProductDescriptionApi")
    {
    }

    public async Task<ListResponse<ProductDescriptionDataModel[]>> Search(
        ProductDescriptionAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductDescriptionAdvancedQuery, ListResponse<ProductDescriptionDataModel[]>>(url, query);
        return response;
    }

    public async Task<ProductDescriptionCompositeModel> GetCompositeModel(
        ProductDescriptionIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<ProductDescriptionCompositeModel>(url);
        return response;
    }

    public async Task<Response<ProductDescriptionDataModel>> Update(ProductDescriptionIdentifier id, ProductDescriptionDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<ProductDescriptionDataModel, Response<ProductDescriptionDataModel>>(url, input);
        return response;
    }

    public async Task<Response<ProductDescriptionDataModel>> Get(ProductDescriptionIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<ProductDescriptionDataModel>>(url);
        return response;
    }

    public async Task<Response<ProductDescriptionDataModel>> Create(ProductDescriptionDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductDescriptionDataModel, Response<ProductDescriptionDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(ProductDescriptionIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

