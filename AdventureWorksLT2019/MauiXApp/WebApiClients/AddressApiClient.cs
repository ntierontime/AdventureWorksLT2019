using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.WebApiClients;
using Framework.Models;
using Framework.MauiX;

namespace AdventureWorksLT2019.MauiXApp.WebApiClients;

public partial class AddressApiClient : WebApiClientBase
{
    public AddressApiClient(WebApiConfig webApiConfig)
        : base(webApiConfig.WebApiRootUrl, "AddressApi")
    {
    }

    public async Task<ListResponse<AddressDataModel[]>> Search(
        AddressAdvancedQuery query)
    {
        const string actionName = nameof(Search);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<AddressAdvancedQuery, ListResponse<AddressDataModel[]>>(url, query);
        return response;
    }

    public async Task<AddressCompositeModel> GetCompositeModel(
        AddressIdentifier id)
    {
        const string actionName = nameof(GetCompositeModel);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<AddressCompositeModel>(url);
        return response;
    }

    public async Task<Response> BulkDelete(List<AddressIdentifier> ids)
    {
        const string actionName = nameof(BulkDelete);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<List<AddressIdentifier>, Response>(url, ids);
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<AddressIdentifier, AddressDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<AddressIdentifier, AddressDataModel> input)
    {
        const string actionName = nameof(MultiItemsCUD);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<MultiItemsCUDRequest<AddressIdentifier, AddressDataModel>, Response<MultiItemsCUDRequest<AddressIdentifier, AddressDataModel>>>(url, input);
        return response;
    }

    public async Task<Response<AddressDataModel>> Update(AddressIdentifier id, AddressDataModel input)
    {
        const string actionName = nameof(Update);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Put<AddressDataModel, Response<AddressDataModel>>(url, input);
        return response;
    }

    public async Task<Response<AddressDataModel>> Get(AddressIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Get<Response<AddressDataModel>>(url);
        return response;
    }

    public async Task<Response<AddressDataModel>> Create(AddressDataModel input)
    {
        const string actionName = nameof(Create);
        string url = GetHttpRequestUrl(actionName);
        var response = await Post<AddressDataModel, Response<AddressDataModel>>(url, input);
        return response;
    }

    public async Task<Response> Delete(AddressIdentifier id)
    {
        const string actionName = nameof(Get);
        string url = GetHttpRequestUrl(actionName, id.GetWebApiRoute());
        var response = await Delete<Response>(url);
        return response;
    }
}

