using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class ProductCategoryApiClient : WebApiClientBase
{
    public ProductCategoryApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "ProductCategoryApi")
    {
    }

    public async Task<ListResponse<ProductCategoryDataModel[]>> Search(
        ProductCategoryAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductCategoryAdvancedQuery, ListResponse<ProductCategoryDataModel[]>>(url, query);
        return response;
    }

    public async Task<ProductCategoryCompositeModel> GetCompositeModel(
        ProductCategoryIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<ProductCategoryCompositeModel>(url);
        return response;
    }

    public async Task<Response<ProductCategoryDataModel>> Update(ProductCategoryIdentifier id, ProductCategoryDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<ProductCategoryDataModel, Response<ProductCategoryDataModel>>(url, input);
        return response;
    }

    public async Task<Response<ProductCategoryDataModel>> Get(ProductCategoryIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<ProductCategoryDataModel>>(url);
        return response;
    }

    public async Task<Response<ProductCategoryDataModel>> Create(ProductCategoryDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductCategoryDataModel, Response<ProductCategoryDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(ProductCategoryIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

