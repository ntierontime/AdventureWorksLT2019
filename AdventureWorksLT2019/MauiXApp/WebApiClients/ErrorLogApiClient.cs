using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;
using System.Text.Json;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class ErrorLogApiClient : WebApiClientBase
{
    public ErrorLogApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "ErrorLogApi")
    {
    }

    public async Task<ListResponse<ErrorLogDataModel[]>> Search(
        ErrorLogAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ErrorLogAdvancedQuery, ListResponse<ErrorLogDataModel[]>>(url, query);
        return response;
    }

    public async Task<ErrorLogCompositeModel> GetCompositeModel(
        ErrorLogIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<ErrorLogCompositeModel>(url);
        return response;
    }

    public async Task<Response> BulkDelete(List<ErrorLogIdentifier> ids)
    {
        const string actionName = nameof(BulkDelete);
        string url = GetHttpRequestUrl(actionName);
        var response = await Put<List<ErrorLogIdentifier>, Response>(url, ids);
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<ErrorLogIdentifier, ErrorLogDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<ErrorLogIdentifier, ErrorLogDataModel> input)
    {
        const string actionName = nameof(MultiItemsCUD);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<MultiItemsCUDRequest<ErrorLogIdentifier, ErrorLogDataModel>, Response<MultiItemsCUDRequest<ErrorLogIdentifier, ErrorLogDataModel>>>(url, input);
        return response;
    }

    public async Task<Response<ErrorLogDataModel>> Update(ErrorLogIdentifier id, ErrorLogDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<ErrorLogDataModel, Response<ErrorLogDataModel>>(url, input);
        return response;
    }

    public async Task<Response<ErrorLogDataModel>> Get(ErrorLogIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<ErrorLogDataModel>>(url);
        return response;
    }

    public async Task<Response<ErrorLogDataModel>> Create(ErrorLogDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<ErrorLogDataModel, Response<ErrorLogDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(ErrorLogIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

