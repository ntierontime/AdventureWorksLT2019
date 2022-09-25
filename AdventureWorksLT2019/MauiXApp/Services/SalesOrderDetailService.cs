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

public class SalesOrderDetailService : DataServiceBase<SalesOrderDetailAdvancedQuery, SalesOrderDetailIdentifier, SalesOrderDetailDataModel>
{

    private readonly SalesOrderDetailApiClient _thisApiClient;
    private readonly CacheDataStatusService _cacheDataStatusService;
    public SalesOrderDetailService(
        SalesOrderDetailApiClient thisApiClient,
        CacheDataStatusService cacheDataStatusService
        )
    {
        _thisApiClient = thisApiClient;
        _cacheDataStatusService = cacheDataStatusService;
    }

    public override async Task<ListResponse<SalesOrderDetailDataModel[]>> Search(
        SalesOrderDetailAdvancedQuery query,
        ObservableQueryOrderBySetting queryOrderBySetting)
    {
        query.OrderBys = ObservableQueryOrderBySetting.GetOrderByExpression(new[] { queryOrderBySetting });
        var response = await _thisApiClient.Search(query);
        return response;
    }

    public override async Task<Response<SalesOrderDetailDataModel>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel input)
    {
        var response = await _thisApiClient.Update(id, input);
        return response;
    }

    public override async Task<Response<SalesOrderDetailDataModel>> Get(SalesOrderDetailIdentifier id)
    {
        var response = await _thisApiClient.Get(id);
        return response;
    }

    public override async Task<Response<SalesOrderDetailDataModel>> Create(SalesOrderDetailDataModel input)
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
                DisplayName = UIStrings.ModifiedDate,
                PropertyName = nameof(SalesOrderDetailDataModel.ModifiedDate),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.History, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<SalesOrderDetailDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                //    return tableQuery;
                //}
            },
            new ObservableQueryOrderBySetting
            {
                IsSelected = false,
                DisplayName = UIStrings.SalesOrderID,
                PropertyName = nameof(SalesOrderDetailDataModel.SalesOrderID),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.SortByAlpha, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<SalesOrderDetailDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.SalesOrderID, direction);
                //    return tableQuery;
                //}
            }
        };
        return queryOrderBySettings;
    }
    public override SalesOrderDetailDataModel GetDefault()
    {
        // TODO: please set default value here
        return new SalesOrderDetailDataModel { ItemUIStatus______ = ItemUIStatus.New };
    }
}

