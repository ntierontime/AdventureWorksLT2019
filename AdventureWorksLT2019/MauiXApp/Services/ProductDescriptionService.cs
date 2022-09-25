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

public class ProductDescriptionService : IDataServiceBase<ProductDescriptionAdvancedQuery, ProductDescriptionIdentifier, ProductDescriptionDataModel>
{

    private readonly ProductDescriptionApiClient _thisApiClient;
    private readonly ProductDescriptionRepository _thisRepository;
    private readonly CacheDataStatusService _cacheDataStatusService;
    public ProductDescriptionService(
        ProductDescriptionApiClient thisApiClient,
        ProductDescriptionRepository thisRepository,
        CacheDataStatusService cacheDataStatusService
        )
    {
        _thisApiClient = thisApiClient;
        _thisRepository = thisRepository;
        _cacheDataStatusService = cacheDataStatusService;
    }

    public async Task CacheDeltaData()
    {
        var query = new ProductDescriptionAdvancedQuery();
        var cachedDataStatusItem = await _cacheDataStatusService.Get(CachedData.ProductDescription.ToString());
        // query.ModifiedDateRangeLower = cachedDataStatusItem.LastSyncDateTime;
        query.PageSize = 10000;// load all
        query.PageIndex = 1;
        var currentQueryOrderBySetting = GetCurrentQueryOrderBySettings();
        query.OrderBys = currentQueryOrderBySetting.ToString();
        var result = await _thisApiClient.Search(query);
        await _thisRepository.Save(result.ResponseBody);
        await _cacheDataStatusService.SyncedServerData(CachedData.ProductDescription.ToString());
    }

    public async Task<ListResponse<ProductDescriptionDataModel[]>> Search(
        ProductDescriptionAdvancedQuery query, ObservableQueryOrderBySetting queryOrderBySetting)
    {
        var result1 = await _thisRepository.GetAllItemsFromTableAsync();

        var result = await _thisRepository.Search(query, queryOrderBySetting);
        var totalCount = await _thisRepository.TotalCount(query);
        var response = new ListResponse<ProductDescriptionDataModel[]>
        {
            Status = System.Net.HttpStatusCode.OK,
            ResponseBody = result.ToArray(),
            Pagination = new PaginationResponse(
                totalCount, result.Count, query.PageIndex, query.PageSize, PaginationOptions.LoadMore)
        };

        return response;
    }

    public async Task<ProductDescriptionCompositeModel> GetCompositeModel(
        ProductDescriptionIdentifier id)
    {
        var response = await _thisApiClient.GetCompositeModel(id);
        return response;
    }

    public async Task<Response> BulkDelete(List<ProductDescriptionIdentifier> ids)
    {
        var response = await _thisApiClient.BulkDelete(ids);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            await _thisRepository.Delete(ids);
        }
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel> input)
    {
        var response = await _thisApiClient.MultiItemsCUD(input);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            if (response.ResponseBody.NewItems != null && response.ResponseBody.NewItems.Count > 0)
                await _thisRepository.Save(response.ResponseBody.NewItems);
            if (response.ResponseBody.UpdateItems != null && response.ResponseBody.UpdateItems.Count > 0)
                await _thisRepository.Save(response.ResponseBody.UpdateItems);
            if (response.ResponseBody.DeleteItems != null && response.ResponseBody.DeleteItems.Count > 0)
                await _thisRepository.Delete(response.ResponseBody.DeleteItems);
        }
        return response;
    }

    public async Task<Response<ProductDescriptionDataModel>> Update(ProductDescriptionIdentifier id, ProductDescriptionDataModel input)
    {
        var response = await _thisApiClient.Update(id, input);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            await _thisRepository.Save(response.ResponseBody);
        }
        return response;
    }

    public async Task<Response<ProductDescriptionDataModel>> Get(ProductDescriptionIdentifier id)
    {
        var response = await _thisApiClient.Get(id);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            await _thisRepository.Save(response.ResponseBody);
        }
        return response;
    }

    public async Task<Response<ProductDescriptionDataModel>> Create(ProductDescriptionDataModel input)
    {
        var response = await _thisApiClient.Create(input);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            await _thisRepository.Save(response.ResponseBody);
        }
        return response;
    }

    public async Task<Response> Delete(ProductDescriptionIdentifier id)
    {
        var response = await _thisApiClient.Delete(id);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            await _thisRepository.Delete(id);
        }
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
                DisplayName = UIStrings.ModifiedDate,
                PropertyName = nameof(ProductDescriptionDataModel.ModifiedDate),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.History, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                SortFunc = (TableQuery<ProductDescriptionDataModel> tableQuery, QueryOrderDirections direction) =>
                {
                    tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                    return tableQuery;
                }
            },
            new ObservableQueryOrderBySetting
            {
                IsSelected = false,
                DisplayName = UIStrings.Description,
                PropertyName = nameof(ProductDescriptionDataModel.Description),
                Direction = QueryOrderDirections.Ascending,
                FontIcon = MaterialIcons.SortByAlpha, FontIconFamily = MaterialIconFamilies.MaterialIconRegular,
                SortFunc = (TableQuery<ProductDescriptionDataModel> tableQuery, QueryOrderDirections direction) =>
                {
                    tableQuery = tableQuery.Sort(t => t.Description, direction);
                    return tableQuery;
                }
            }
        };
        return queryOrderBySettings;
    }
    public ProductDescriptionDataModel GetDefault()
    {
        // TODO: please set default value here
        return new ProductDescriptionDataModel { ItemUIStatus______ = ItemUIStatus.New };
    }
}

