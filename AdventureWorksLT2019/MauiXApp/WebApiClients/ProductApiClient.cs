using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class ProductApiClient : WebApiClientBase
{
    public ProductApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "ProductApi")
    {
    }

    public async Task<ListResponse<ProductDataModel[]>> Search(
        ProductAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductAdvancedQuery, ListResponse<ProductDataModel[]>>(url, query);
        return response;
    }

    public async Task<ProductCompositeModel> GetCompositeModel(
        ProductIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<ProductCompositeModel>(url);
        return response;
    }

    public async Task<Response> BulkDelete(List<ProductIdentifier> ids)
    {
        const string actionName = nameof(BulkDelete);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<List<ProductIdentifier>, Response>(url, ids);
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<ProductIdentifier, ProductDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<ProductIdentifier, ProductDataModel> input)
    {
        const string actionName = nameof(MultiItemsCUD);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<MultiItemsCUDRequest<ProductIdentifier, ProductDataModel>, Response<MultiItemsCUDRequest<ProductIdentifier, ProductDataModel>>>(url, input);
        return response;
    }

    public async Task<Response<ProductDataModel>> Update(ProductIdentifier id, ProductDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<ProductDataModel, Response<ProductDataModel>>(url, input);
        return response;
    }

    public async Task<Response<ProductDataModel>> Get(ProductIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<ProductDataModel>>(url);
        return response;
    }

    public async Task<Response<ProductDataModel>> Create(ProductDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ProductDataModel, Response<ProductDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(ProductIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

