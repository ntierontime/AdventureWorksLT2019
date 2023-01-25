using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;
using System.Text.Json;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class SalesOrderDetailApiClient : WebApiClientBase
{
    public SalesOrderDetailApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "SalesOrderDetailApi")
    {
    }

    public async Task<ListResponse<SalesOrderDetailDataModel[]>> Search(
        SalesOrderDetailAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<SalesOrderDetailAdvancedQuery, ListResponse<SalesOrderDetailDataModel[]>>(url, query);
        return response;
    }

    public async Task<SalesOrderDetailCompositeModel> GetCompositeModel(
        SalesOrderDetailIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<SalesOrderDetailCompositeModel>(url);
        return response;
    }

    public async Task<Response> BulkDelete(List<SalesOrderDetailIdentifier> ids)
    {
        const string actionName = nameof(BulkDelete);
        string url = GetHttpRequestUrl(actionName);
        var response = await Put<List<SalesOrderDetailIdentifier>, Response>(url, ids);
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel> input)
    {
        const string actionName = nameof(MultiItemsCUD);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel>, Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel>>>(url, input);
        return response;
    }

    public async Task<Response<SalesOrderDetailDataModel>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<SalesOrderDetailDataModel, Response<SalesOrderDetailDataModel>>(url, input);
        return response;
    }

    public async Task<Response<SalesOrderDetailDataModel>> Get(SalesOrderDetailIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<SalesOrderDetailDataModel>>(url);
        return response;
    }

    public async Task<Response<SalesOrderDetailDataModel>> Create(SalesOrderDetailDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<SalesOrderDetailDataModel, Response<SalesOrderDetailDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(SalesOrderDetailIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

