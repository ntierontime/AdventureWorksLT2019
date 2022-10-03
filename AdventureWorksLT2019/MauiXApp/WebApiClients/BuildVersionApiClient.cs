using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class BuildVersionApiClient : WebApiClientBase
{
    public BuildVersionApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "BuildVersionApi")
    {
    }

    public async Task<ListResponse<BuildVersionDataModel[]>> Search(
        BuildVersionAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<BuildVersionAdvancedQuery, ListResponse<BuildVersionDataModel[]>>(url, query);
        return response;
    }

    public async Task<BuildVersionCompositeModel> GetCompositeModel(
        BuildVersionIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<BuildVersionCompositeModel>(url);
        return response;
    }

    public async Task<Response> BulkDelete(List<BuildVersionIdentifier> ids)
    {
        const string actionName = nameof(BulkDelete);
        string url = GetHttpRequestUrl(actionName);
        var response = await Put<List<BuildVersionIdentifier>, Response>(url, ids);
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel> input)
    {
        const string actionName = nameof(MultiItemsCUD);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>, Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>>(url, input);
        return response;
    }

    public async Task<Response<BuildVersionDataModel>> Update(BuildVersionIdentifier id, BuildVersionDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<BuildVersionDataModel, Response<BuildVersionDataModel>>(url, input);
        return response;
    }

    public async Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<BuildVersionDataModel>>(url);
        return response;
    }

    public async Task<Response<BuildVersionDataModel>> Create(BuildVersionDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<BuildVersionDataModel, Response<BuildVersionDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(BuildVersionIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

