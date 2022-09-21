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

public class CustomerService : IDataServiceBase<CustomerAdvancedQuery, CustomerIdentifier, CustomerDataModel>
{

    private readonly CustomerApiClient _thisApiClient;
    private readonly CustomerRepository _thisRepository;
    private readonly CacheDataStatusService _cacheDataStatusService;
    public CustomerService(
        CustomerApiClient thisApiClient,
        CustomerRepository thisRepository,
        CacheDataStatusService cacheDataStatusService
        )
    {
        _thisApiClient = thisApiClient;
        _thisRepository = thisRepository;
        _cacheDataStatusService = cacheDataStatusService;
    }

    public async Task CacheDeltaData()
    {
        var query = new CustomerAdvancedQuery();
        var cachedDataStatusItem = await _cacheDataStatusService.Get(CachedData.Customer.ToString());
        // query.ModifiedDateRangeLower = cachedDataStatusItem.LastSyncDateTime;
        query.PageSize = 10000;// load all
        query.PageIndex = 1;
        var currentQueryOrderBySetting = GetCurrentQueryOrderBySettings();
        query.OrderBys = currentQueryOrderBySetting.ToString();
        var result = await _thisApiClient.Search(query);
        await _thisRepository.Save(result.ResponseBody);
        await _cacheDataStatusService.SyncedServerData(CachedData.Customer.ToString());
    }

    public async Task<ListResponse<CustomerDataModel[]>> Search(
        CustomerAdvancedQuery query, ObservableQueryOrderBySetting queryOrderBySetting)
    {
        var result1 = await _thisRepository.GetAllItemsFromTableAsync();

        var result = await _thisRepository.Search(query, queryOrderBySetting);
        var totalCount = await _thisRepository.TotalCount(query);
        var response = new ListResponse<CustomerDataModel[]>
        {
            Status = System.Net.HttpStatusCode.OK,
            ResponseBody = result.ToArray(),
            Pagination = new PaginationResponse(
                totalCount, result.Count, query.PageIndex, query.PageSize, PaginationOptions.LoadMore)
        };

        return response;
    }

    public async Task<CustomerCompositeModel> GetCompositeModel(
        CustomerIdentifier id)
    {
        var response = await _thisApiClient.GetCompositeModel(id);
        return response;
    }

    public async Task<Response> BulkDelete(List<CustomerIdentifier> ids)
    {
        var response = await _thisApiClient.BulkDelete(ids);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            await _thisRepository.Delete(ids);
        }
        return response;
    }

    public async Task<ListResponse<CustomerDataModel[]>> BulkUpdate(BatchActionRequest<CustomerIdentifier, CustomerDataModel> data)
    {
        var response = await _thisApiClient.BulkUpdate(data);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            await _thisRepository.Save(response.ResponseBody?.ToList());
        }
        return response;
    }

    public async Task<Response<MultiItemsCUDRequest<CustomerIdentifier, CustomerDataModel>>> MultiItemsCUD(
        MultiItemsCUDRequest<CustomerIdentifier, CustomerDataModel> input)
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

    public async Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input)
    {
        var response = await _thisApiClient.Update(id, input);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            await _thisRepository.Save(response.ResponseBody);
        }
        return response;
    }

    public async Task<Response<CustomerDataModel>> Get(CustomerIdentifier id)
    {
        var response = await _thisApiClient.Get(id);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            await _thisRepository.Save(response.ResponseBody);
        }
        return response;
    }

    public async Task<Response<CustomerDataModel>> Create(CustomerDataModel input)
    {
        var response = await _thisApiClient.Create(input);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            await _thisRepository.Save(response.ResponseBody);
        }
        return response;
    }

    public async Task<Response> Delete(CustomerIdentifier id)
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
                PropertyName = nameof(CustomerDataModel.ModifiedDate),
                Direction = QueryOrderDirections.Ascending,
                //FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                SortFunc = (TableQuery<CustomerDataModel> tableQuery, QueryOrderDirections direction) =>
                {
                    tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                    return tableQuery;
                }
            },
            new ObservableQueryOrderBySetting
            {
                IsSelected = false,
                DisplayName = UIStrings.ModifiedDate,
                PropertyName = nameof(CustomerDataModel.ModifiedDate),
                Direction = QueryOrderDirections.Descending,
                //FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                SortFunc = (TableQuery<CustomerDataModel> tableQuery, QueryOrderDirections direction) =>
                {
                    tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                    return tableQuery;
                }
            }
        };
        return queryOrderBySettings;
    }
    public CustomerDataModel GetDefault()
    {
        // TODO: please set default value here
        return new CustomerDataModel { ItemUIStatus______ = ItemUIStatus.New };
    }
}

