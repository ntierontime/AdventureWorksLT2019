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

public class AddressService : DataServiceBase<AddressAdvancedQuery, AddressIdentifier, AddressDataModel>
{

    private readonly AddressApiClient _thisApiClient;
    private readonly CacheDataStatusService _cacheDataStatusService;
    public AddressService(
        AddressApiClient thisApiClient,
        CacheDataStatusService cacheDataStatusService
        )
    {
        _thisApiClient = thisApiClient;
        _cacheDataStatusService = cacheDataStatusService;
    }

    public override async Task<ListResponse<AddressDataModel[]>> Search(
        AddressAdvancedQuery query,
        ObservableQueryOrderBySetting queryOrderBySetting)
    {
        query.OrderBys = ObservableQueryOrderBySetting.GetOrderByExpression(new[] { queryOrderBySetting });
        var response = await _thisApiClient.Search(query);
        return response;
    }

    public async Task<AddressCompositeModel> GetCompositeModel(
        AddressIdentifier id)
    {
        var response = await _thisApiClient.GetCompositeModel(id);
        return response;
    }

    public override async Task<Response<AddressDataModel>> Update(AddressIdentifier id, AddressDataModel input)
    {
        var response = await _thisApiClient.Update(id, input);
        return response;
    }

    public override async Task<Response<AddressDataModel>> Get(AddressIdentifier id)
    {
        var response = await _thisApiClient.Get(id);
        return response;
    }

    public override async Task<Response<AddressDataModel>> Create(AddressDataModel input)
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
                PropertyName = nameof(AddressDataModel.ModifiedDate),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.History, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<AddressDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                //    return tableQuery;
                //}
            },
            new ObservableQueryOrderBySetting
            {
                IsSelected = false,
                DisplayName = UIStrings.AddressLine1,
                PropertyName = nameof(AddressDataModel.AddressLine1),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.SortByAlpha, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<AddressDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.AddressLine1, direction);
                //    return tableQuery;
                //}
            }
        };
        return queryOrderBySettings;
    }
    public override AddressDataModel GetDefault()
    {
        // TODO: please set default value here
        return new AddressDataModel { ItemUIStatus______ = ItemUIStatus.New };
    }
}

