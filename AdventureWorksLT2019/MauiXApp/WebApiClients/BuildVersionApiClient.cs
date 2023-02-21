using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;
using System.Text.Json;

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
}

