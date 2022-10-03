using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class SalesOrderHeaderApiClient : WebApiClientBase
{
    public SalesOrderHeaderApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "SalesOrderHeaderApi")
    {
    }

    public async Task<ListResponse<SalesOrderHeaderDataModel[]>> Search(
        SalesOrderHeaderAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<SalesOrderHeaderAdvancedQuery, ListResponse<SalesOrderHeaderDataModel[]>>(url, query);
        return response;
    }

    public async Task<SalesOrderHeaderCompositeModel> GetCompositeModel(
        SalesOrderHeaderIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<SalesOrderHeaderCompositeModel>(url);
        return response;
    }

    public async Task<Response> BulkDelete(List<SalesOrderHeaderIdentifier> ids)
    {
        const string actionName = nameof(BulkDelete);
        string url = GetHttpRequestUrl(actionName);
        var response = await Put<List<SalesOrderHeaderIdentifier>, Response>(url, ids);
        return response;
    }

    public async Task<ListResponse<SalesOrderHeaderDataModel[]>> BulkUpdate(BatchActionRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel> data)
    {
        const string actionName = nameof(BulkUpdate);
        string url = GetHttpRequestUrl(actionName);
        var response = await Put<BatchActionRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel>, ListResponse<SalesOrderHeaderDataModel[]>>(url, data);
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel> input)
    {
        const string actionName = nameof(MultiItemsCUD);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel>, Response<MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel>>>(url, input);
        return response;
    }

    public async Task<Response<SalesOrderHeaderDataModel>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<SalesOrderHeaderDataModel, Response<SalesOrderHeaderDataModel>>(url, input);
        return response;
    }

    public async Task<Response<SalesOrderHeaderDataModel>> Get(SalesOrderHeaderIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<SalesOrderHeaderDataModel>>(url);
        return response;
    }

    public async Task<Response<SalesOrderHeaderDataModel>> Create(SalesOrderHeaderDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<SalesOrderHeaderDataModel, Response<SalesOrderHeaderDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(SalesOrderHeaderIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

