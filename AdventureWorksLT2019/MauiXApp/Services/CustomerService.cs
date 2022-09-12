using SQLite;
using Framework.MauiX.SQLite;

namespace AdventureWorksLT2019.MauiXApp.Services
{
    public class CustomerService
    {
        private readonly AdventureWorksLT2019.MauiXApp.WebApiClients.CustomerApiClient _customerApiClient;
        private readonly AdventureWorksLT2019.MauiXApp.SQLite.CustomerRepository _customerRepository;
        private readonly Framework.MauiX.SQLite.CacheDataStatusService _cacheDataStatusService;
        public CustomerService(
            AdventureWorksLT2019.MauiXApp.WebApiClients.CustomerApiClient customerApiClient,
            AdventureWorksLT2019.MauiXApp.SQLite.CustomerRepository customerRepository,
            Framework.MauiX.SQLite.CacheDataStatusService cacheDataStatusService
            )
        {
            _customerApiClient = customerApiClient;
            _customerRepository = customerRepository;
            _cacheDataStatusService = cacheDataStatusService;
        }

        public static Framework.MauiX.DataModels.ObservableQueryOrderBySetting GetCurrentQueryOrderBySettings()
        {
            var queryOrderBySettings = GetQueryOrderBySettings();
            var currentQueryOrderBySetting = queryOrderBySettings.First(t => t.IsSelected);
            // TODO: should read from CacheDataStatusItem.CurrentOrderBy, and Parse
            return currentQueryOrderBySetting;
        }

        public static List<Framework.MauiX.DataModels.ObservableQueryOrderBySetting> GetQueryOrderBySettings()
        {
            var queryOrderBySettings = new List<Framework.MauiX.DataModels.ObservableQueryOrderBySetting> {
                new Framework.MauiX.DataModels.ObservableQueryOrderBySetting
                {
                    IsSelected = true,
                    DisplayName = AdventureWorksLT2019.Resx.Resources.UIStrings.ModifiedDate,
                    PropertyName = nameof(AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel.ModifiedDate),
                    Direction = Framework.Models.QueryOrderDirections.Ascending, 
                    //FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                    SortFunc = (TableQuery<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> tableQuery, Framework.Models.QueryOrderDirections direction) =>
                    {
                        tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                        return tableQuery;
                    }
                },
                new Framework.MauiX.DataModels.ObservableQueryOrderBySetting
                {
                    IsSelected = false,
                    DisplayName = AdventureWorksLT2019.Resx.Resources.UIStrings.ModifiedDate,
                    PropertyName = nameof(AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel.ModifiedDate),
                    Direction = Framework.Models.QueryOrderDirections.Descending, 
                    //FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                    SortFunc = (TableQuery<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> tableQuery, Framework.Models.QueryOrderDirections direction) =>
                    {
                        tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                        return tableQuery;
                    }
                }
            };
            return queryOrderBySettings;
        }

        public async Task CacheDeltaData()
        {
            var query = new AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery();
            var cachedDataStatusItem = await _cacheDataStatusService.Get(AdventureWorksLT2019.MauiXApp.Services.Common.CachedData.Customer.ToString());
            query.ModifiedDateRangeLower = cachedDataStatusItem.LastSyncDateTime;
            query.PageSize = 10000;// load all
            query.PageIndex = 1;
            var currentQueryOrderBySetting = GetCurrentQueryOrderBySettings();
            query.OrderBys = currentQueryOrderBySetting.ToString();
            var result = await _customerApiClient.Search(query);
            await _customerRepository.Save(result.ResponseBody);
            await _cacheDataStatusService.SyncedServerData(AdventureWorksLT2019.MauiXApp.Services.Common.CachedData.Customer.ToString());
        }

        public async Task<Framework.Models.ListResponse<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel[]>> Search(
            AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery query, Framework.MauiX.DataModels.ObservableQueryOrderBySetting queryOrderBySetting)
        {
            var result1 = await _customerRepository.GetAllItemsFromTableAsync();

            var result = await _customerRepository.Search(query, queryOrderBySetting);
            var totalCount = await _customerRepository.TotalCount(query);
            var response = new Framework.Models.ListResponse<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel[]>
            {
                Status = System.Net.HttpStatusCode.OK,
                ResponseBody = result.ToArray(),
                Pagination = new Framework.Models.PaginationResponse(
                    totalCount, result.Count, query.PageIndex, query.PageSize, Framework.Models.PaginationOptions.LoadMore)
            };

            return response;
        }

        public async Task<AdventureWorksLT2019.MauiXApp.DataModels.CustomerCompositeModel> GetCompositeModel(
            AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier id)
        {
            var response = await _customerApiClient.GetCompositeModel(id);
            return response;
        }

        public async Task<Framework.Models.Response> BulkDelete(List<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier> ids)
        {
            var response = await _customerApiClient.BulkDelete(ids);
            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                await _customerRepository.Delete(ids);
            }
            return response;
        }

        public async Task<Framework.Models.ListResponse<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel[]>> BulkUpdate(Framework.Models.BatchActionRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> data)
        {
            var response = await _customerApiClient.BulkUpdate(data);
            if(response.Status == System.Net.HttpStatusCode.OK)
            {
                await _customerRepository.Save(response.ResponseBody?.ToList());
            }
            return response;
        }

        public async Task<Framework.Models.Response<Framework.Models.MultiItemsCUDRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>>> MultiItemsCUD(
            Framework.Models.MultiItemsCUDRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> input)
        {
            var response = await _customerApiClient.MultiItemsCUD(input);
            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                if(response.ResponseBody.NewItems != null && response.ResponseBody.NewItems.Count > 0)
                    await _customerRepository.Save(response.ResponseBody.NewItems);
                if (response.ResponseBody.UpdateItems != null && response.ResponseBody.UpdateItems.Count > 0)
                    await _customerRepository.Save(response.ResponseBody.UpdateItems);
                if (response.ResponseBody.DeleteItems != null && response.ResponseBody.DeleteItems.Count > 0)
                    await _customerRepository.Delete(response.ResponseBody.DeleteItems);
            }
            return response;
        }

        public async Task<Framework.Models.Response<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>> Update(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier id, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel input)
        {
            var response = await _customerApiClient.Update(id, input);
            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                await _customerRepository.Save(response.ResponseBody);
            }
            return response;
        }

        public async Task<Framework.Models.Response<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>> Get(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier id)
        {
            var response = await _customerApiClient.Get(id);
            return response;
        }

        public async Task<Framework.Models.Response<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>> Create(AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel input)
        {
            var response = await _customerApiClient.Create(input);
            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                await _customerRepository.Save(response.ResponseBody);
            }
            return response;
        }

        public static AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel GetDefault()
        {
            // TODO: please set default value here
            return new AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel { ItemUIStatus______ = Framework.Models.ItemUIStatus.New };
        }

        public async Task<Framework.Models.Response> Delete(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier id)
        {
            var response = await _customerApiClient.Delete(id);
            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                await _customerRepository.Delete(id);
            }
            return response;
        }
    }
}
