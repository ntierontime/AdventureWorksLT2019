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

public class BuildVersionService : IDataServiceBase<BuildVersionAdvancedQuery, BuildVersionIdentifier, BuildVersionDataModel>
{

    private readonly BuildVersionApiClient _thisApiClient;
    private readonly CacheDataStatusService _cacheDataStatusService;
    public BuildVersionService(
        BuildVersionApiClient thisApiClient,
        CacheDataStatusService cacheDataStatusService
        )
    {
        _thisApiClient = thisApiClient;
        _cacheDataStatusService = cacheDataStatusService;
    }

    public async Task<ListResponse<BuildVersionDataModel[]>> Search(
        BuildVersionAdvancedQuery query,
        ObservableQueryOrderBySetting queryOrderBySetting)
    {
        query.OrderBys = ObservableQueryOrderBySetting.GetOrderByExpression(new[] { queryOrderBySetting });
        var response = await _thisApiClient.Search(query);
        return response;
    }

    public async Task<BuildVersionCompositeModel> GetCompositeModel(
        BuildVersionIdentifier id)
    {
        var response = await _thisApiClient.GetCompositeModel(id);
        return response;
    }

    public async Task<Response> BulkDelete(List<BuildVersionIdentifier> ids)
    {
        var response = await _thisApiClient.BulkDelete(ids);
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel> input)
    {
        var response = await _thisApiClient.MultiItemsCUD(input);
        return response;
    }

    public async Task<Response<BuildVersionDataModel>> Update(BuildVersionIdentifier id, BuildVersionDataModel input)
    {
        var response = await _thisApiClient.Update(id, input);
        return response;
    }

    public async Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id)
    {
        var response = await _thisApiClient.Get(id);
        return response;
    }

    public async Task<Response<BuildVersionDataModel>> Create(BuildVersionDataModel input)
    {
        var response = await _thisApiClient.Create(input);
        return response;
    }

    public async Task<Response> Delete(BuildVersionIdentifier id)
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
                DisplayName = UIStrings.VersionDate,
                PropertyName = nameof(BuildVersionDataModel.VersionDate),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.History, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<BuildVersionDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.VersionDate, direction);
                //    return tableQuery;
                //}
            },
            new ObservableQueryOrderBySetting
            {
                IsSelected = false,
                DisplayName = UIStrings.Database_Version,
                PropertyName = nameof(BuildVersionDataModel.Database_Version),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.SortByAlpha, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<BuildVersionDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.Database_Version, direction);
                //    return tableQuery;
                //}
            }
        };
        return queryOrderBySettings;
    }
    public BuildVersionDataModel GetDefault()
    {
        // TODO: please set default value here
        return new BuildVersionDataModel { ItemUIStatus______ = ItemUIStatus.New };
    }
}

