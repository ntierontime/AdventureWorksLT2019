using AdventureWorksLT2019.MauiXApp.SQLite;
using Framework.MauiX.SQLite;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using Framework.MauiX.Icons;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.WebApiClients;
using Framework.MauiX.DataModels;
using AdventureWorksLT2019.Resx.Resources;
using Framework.MauiX.Services;
using Framework.Models;
using SQLite;

namespace AdventureWorksLT2019.MauiXApp.Services;

public class CustomerService : DataServiceBase<CustomerAdvancedQuery, CustomerIdentifier, CustomerDataModel>
{

    private readonly CustomerApiClient _thisApiClient;
    private readonly CacheDataStatusService _cacheDataStatusService;
    public CustomerService(
        CustomerApiClient thisApiClient,
        CacheDataStatusService cacheDataStatusService
        )
    {
        _thisApiClient = thisApiClient;
        _cacheDataStatusService = cacheDataStatusService;
    }

    public override async Task<ListResponse<CustomerDataModel[]>> Search(
        CustomerAdvancedQuery query,
        ObservableQueryOrderBySetting queryOrderBySetting)
    {
        query.OrderBys = ObservableQueryOrderBySetting.GetOrderByExpression(new[] { queryOrderBySetting });
        var response = await _thisApiClient.Search(query);
        return response;
    }

    public async Task<CustomerCompositeModel> GetCompositeModel(
        CustomerIdentifier id)
    {
        var response = await _thisApiClient.GetCompositeModel(id);
        return response;
    }

    public override async Task<Response> BulkDelete(List<CustomerIdentifier> ids)
    {
        var response = await _thisApiClient.BulkDelete(ids);
        return response;
    }

    public override async Task<ListResponse<CustomerDataModel[]>> BulkUpdate(BatchActionRequest<CustomerIdentifier, CustomerDataModel> data)
    {
        var response = await _thisApiClient.BulkUpdate(data);
        return response;
    }

    public override async Task<Response<MultiItemsCUDRequest<CustomerIdentifier, CustomerDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<CustomerIdentifier, CustomerDataModel> input)
    {
        var response = await _thisApiClient.MultiItemsCUD(input);
        return response;
    }

    public override async Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input)
    {
        var response = await _thisApiClient.Update(id, input);
        return response;
    }

    public override async Task<Response<CustomerDataModel>> Get(CustomerIdentifier id)
    {
        var response = await _thisApiClient.Get(id);
        return response;
    }

    public override async Task<Response<CustomerDataModel>> Create(CustomerDataModel input)
    {
        var response = await _thisApiClient.Create(input);
        return response;
    }

    public override async Task<Response> Delete(CustomerIdentifier id)
    {
        var response = await _thisApiClient.Delete(id);
        return response;
    }

    public ObservableQueryOrderBySetting GetCurrentQueryOrderBySettings()
    {
        var queryOrderBySettings = GetQueryOrderBySettings();
        var currentQueryOrderBySetting = queryOrderBySettings.First(t => t.IsSelected);
        // TODO: should read from CacheDataStatusItem.CurrentOrderBy, and Parse
        return currentQueryOrderBySetting;
    }

    public override List<ObservableQueryOrderBySetting> GetQueryOrderBySettings()
    {
        var queryOrderBySettings = new List<ObservableQueryOrderBySetting> {
            new ObservableQueryOrderBySetting
            {
                IsSelected = true,
                DisplayName = UIStrings.ModifiedDate,
                PropertyName = nameof(CustomerDataModel.ModifiedDate),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.History, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<CustomerDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                //    return tableQuery;
                //}
            },
            new ObservableQueryOrderBySetting
            {
                IsSelected = false,
                DisplayName = UIStrings.Title,
                PropertyName = nameof(CustomerDataModel.Title),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.SortByAlpha, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<CustomerDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.Title, direction);
                //    return tableQuery;
                //}
            }
        };
        return queryOrderBySettings;
    }
    public override CustomerDataModel GetDefault()
    {
        // TODO: please set default value here
        return new CustomerDataModel { ItemUIStatus______ = ItemUIStatus.New };
    }
}

