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

public class SalesOrderHeaderService : DataServiceBase<SalesOrderHeaderAdvancedQuery, SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel>
{

    private readonly SalesOrderHeaderApiClient _thisApiClient;
    private readonly CacheDataStatusService _cacheDataStatusService;
    public SalesOrderHeaderService(
        SalesOrderHeaderApiClient thisApiClient,
        CacheDataStatusService cacheDataStatusService
        )
    {
        _thisApiClient = thisApiClient;
        _cacheDataStatusService = cacheDataStatusService;
    }

    public override async Task<ListResponse<SalesOrderHeaderDataModel[]>> Search(
        SalesOrderHeaderAdvancedQuery query,
        ObservableQueryOrderBySetting queryOrderBySetting)
    {
        query.OrderBys = ObservableQueryOrderBySetting.GetOrderByExpression(new[] { queryOrderBySetting });
        var response = await _thisApiClient.Search(query);
        return response;
    }

    public async Task<SalesOrderHeaderCompositeModel> GetCompositeModel(
        SalesOrderHeaderIdentifier id)
    {
        var response = await _thisApiClient.GetCompositeModel(id);
        return response;
    }

    public override async Task<ListResponse<SalesOrderHeaderDataModel[]>> BulkUpdate(BatchActionRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel> data)
    {
        var response = await _thisApiClient.BulkUpdate(data);
        return response;
    }

    public override async Task<Response<SalesOrderHeaderDataModel>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel input)
    {
        var response = await _thisApiClient.Update(id, input);
        return response;
    }

    public override async Task<Response<SalesOrderHeaderDataModel>> Get(SalesOrderHeaderIdentifier id)
    {
        var response = await _thisApiClient.Get(id);
        return response;
    }

    public override async Task<Response<SalesOrderHeaderDataModel>> Create(SalesOrderHeaderDataModel input)
    {
        var response = await _thisApiClient.Create(input);
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
                DisplayName = UIStrings.OrderDate,
                PropertyName = nameof(SalesOrderHeaderDataModel.OrderDate),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.History, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<SalesOrderHeaderDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.OrderDate, direction);
                //    return tableQuery;
                //}
            },
            new ObservableQueryOrderBySetting
            {
                IsSelected = false,
                DisplayName = UIStrings.SalesOrderNumber,
                PropertyName = nameof(SalesOrderHeaderDataModel.SalesOrderNumber),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.SortByAlpha, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<SalesOrderHeaderDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.SalesOrderNumber, direction);
                //    return tableQuery;
                //}
            }
        };
        return queryOrderBySettings;
    }
    public override SalesOrderHeaderDataModel GetDefault()
    {
        // TODO: please set default value here
        return new SalesOrderHeaderDataModel { ItemUIStatus______ = ItemUIStatus.New };
    }
}

