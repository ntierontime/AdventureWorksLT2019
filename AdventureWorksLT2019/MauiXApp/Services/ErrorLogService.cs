using AdventureWorksLT2019.MauiXApp.SQLite;
using Framework.MauiX.SQLite;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.WebApiClients;
using Framework.MauiX.DataModels;
using AdventureWorksLT2019.Resx.Resources;
using Framework.MauiX.Services;
using Framework.Models;
using SQLite;

namespace AdventureWorksLT2019.MauiXApp.Services;

public class ErrorLogService : IDataServiceBase<ErrorLogAdvancedQuery, ErrorLogIdentifier, ErrorLogDataModel>
{

    private readonly ErrorLogApiClient _thisApiClient;
    private readonly CacheDataStatusService _cacheDataStatusService;
    public ErrorLogService(
        ErrorLogApiClient thisApiClient,
        CacheDataStatusService cacheDataStatusService
        )
    {
        _thisApiClient = thisApiClient;
        _cacheDataStatusService = cacheDataStatusService;
    }

    public async Task<ListResponse<ErrorLogDataModel[]>> Search(
        ErrorLogAdvancedQuery query,
        ObservableQueryOrderBySetting queryOrderBySetting)
    {
        query.OrderBys = ObservableQueryOrderBySetting.GetOrderByExpression(new[] { queryOrderBySetting });
        var response = await _thisApiClient.Search(query);
        return response;
    }

    public async Task<ErrorLogCompositeModel> GetCompositeModel(
        ErrorLogIdentifier id)
    {
        var response = await _thisApiClient.GetCompositeModel(id);
        return response;
    }

    public async Task<Response> BulkDelete(List<ErrorLogIdentifier> ids)
    {
        var response = await _thisApiClient.BulkDelete(ids);
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<ErrorLogIdentifier, ErrorLogDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<ErrorLogIdentifier, ErrorLogDataModel> input)
    {
        var response = await _thisApiClient.MultiItemsCUD(input);
        return response;
    }

    public async Task<Response<ErrorLogDataModel>> Update(ErrorLogIdentifier id, ErrorLogDataModel input)
    {
        var response = await _thisApiClient.Update(id, input);
        return response;
    }

    public async Task<Response<ErrorLogDataModel>> Get(ErrorLogIdentifier id)
    {
        var response = await _thisApiClient.Get(id);
        return response;
    }

    public async Task<Response<ErrorLogDataModel>> Create(ErrorLogDataModel input)
    {
        var response = await _thisApiClient.Create(input);
        return response;
    }

    public async Task<Response> Delete(ErrorLogIdentifier id)
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

    public List<ObservableQueryOrderBySetting> GetQueryOrderBySettings()
    {
        var queryOrderBySettings = new List<ObservableQueryOrderBySetting> {
            new ObservableQueryOrderBySetting
            {
                IsSelected = true,
                DisplayName = UIStrings.ErrorTime,
                PropertyName = nameof(ErrorLogDataModel.ErrorTime),
                Direction = QueryOrderDirections.Ascending,
                //FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                //SortFunc = (TableQuery<ErrorLogDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.ErrorTime, direction);
                //    return tableQuery;
                //}
            },
            new ObservableQueryOrderBySetting
            {
                IsSelected = false,
                DisplayName = UIStrings.ErrorTime,
                PropertyName = nameof(ErrorLogDataModel.ErrorTime),
                Direction = QueryOrderDirections.Descending,
                //FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                //SortFunc = (TableQuery<ErrorLogDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.ErrorTime, direction);
                //    return tableQuery;
                //}
            }
        };
        return queryOrderBySettings;
    }
    public ErrorLogDataModel GetDefault()
    {
        // TODO: please set default value here
        return new ErrorLogDataModel { ItemUIStatus______ = ItemUIStatus.New };
    }
}

