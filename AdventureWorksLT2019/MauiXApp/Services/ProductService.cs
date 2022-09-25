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

public class ProductService : DataServiceBase<ProductAdvancedQuery, ProductIdentifier, ProductDataModel>
{

    private readonly ProductApiClient _thisApiClient;
    private readonly CacheDataStatusService _cacheDataStatusService;
    public ProductService(
        ProductApiClient thisApiClient,
        CacheDataStatusService cacheDataStatusService
        )
    {
        _thisApiClient = thisApiClient;
        _cacheDataStatusService = cacheDataStatusService;
    }

    public override async Task<ListResponse<ProductDataModel[]>> Search(
        ProductAdvancedQuery query,
        ObservableQueryOrderBySetting queryOrderBySetting)
    {
        query.OrderBys = ObservableQueryOrderBySetting.GetOrderByExpression(new[] { queryOrderBySetting });
        var response = await _thisApiClient.Search(query);
        return response;
    }

    public async Task<ProductCompositeModel> GetCompositeModel(
        ProductIdentifier id)
    {
        var response = await _thisApiClient.GetCompositeModel(id);
        return response;
    }

    public override async Task<Response<ProductDataModel>> Update(ProductIdentifier id, ProductDataModel input)
    {
        var response = await _thisApiClient.Update(id, input);
        return response;
    }

    public override async Task<Response<ProductDataModel>> Get(ProductIdentifier id)
    {
        var response = await _thisApiClient.Get(id);
        return response;
    }

    public override async Task<Response<ProductDataModel>> Create(ProductDataModel input)
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
                DisplayName = UIStrings.SellStartDate,
                PropertyName = nameof(ProductDataModel.SellStartDate),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.History, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<ProductDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.SellStartDate, direction);
                //    return tableQuery;
                //}
            },
            new ObservableQueryOrderBySetting
            {
                IsSelected = false,
                DisplayName = UIStrings.Name,
                PropertyName = nameof(ProductDataModel.Name),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.SortByAlpha, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                //SortFunc = (TableQuery<ProductDataModel> tableQuery, QueryOrderDirections direction) =>
                //{
                //    tableQuery = tableQuery.Sort(t => t.Name, direction);
                //    return tableQuery;
                //}
            }
        };
        return queryOrderBySettings;
    }
    public override ProductDataModel GetDefault()
    {
        // TODO: please set default value here
        return new ProductDataModel { ItemUIStatus______ = ItemUIStatus.New };
    }
}

