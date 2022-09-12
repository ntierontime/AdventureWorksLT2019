namespace AdventureWorksLT2019.MauiXApp.Services
{
    public class CustomerService
    {
        private readonly AdventureWorksLT2019.MauiXApp.WebApiClients.CustomerApiClient _customerApiClient;
        
        public CustomerService(
            AdventureWorksLT2019.MauiXApp.WebApiClients.CustomerApiClient customerApiClient)
        {
            _customerApiClient = customerApiClient;
        }

        public async Task<Framework.Models.ListResponse<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel[]>> Search(
            AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery query,
            Framework.MauiX.DataModels.ObservableQueryOrderBySetting queryOrderBySetting)
        {
            query.OrderBys = Framework.MauiX.DataModels.ObservableQueryOrderBySetting.GetOrderByExpression(new [] { queryOrderBySetting });
            var response = await _customerApiClient.Search(query);
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
            return response;
        }

        public async Task<Framework.Models.ListResponse<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel[]>> BulkUpdate(Framework.Models.BatchActionRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> data)
        {
            var response = await _customerApiClient.BulkUpdate(data);
            return response;
        }

        public async Task<Framework.Models.Response<Framework.Models.MultiItemsCUDRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>>> MultiItemsCUD(
            Framework.Models.MultiItemsCUDRequest<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> input)
        {
            var response = await _customerApiClient.MultiItemsCUD(input);
            return response;
        }

        public async Task<Framework.Models.Response<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>> Update(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier id, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel input)
        {
            var response = await _customerApiClient.Update(id, input);
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
            return response;
        }

        public AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel GetDefault()
        {
            // TODO: please set default value here
            return new AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel { ItemUIStatus______ = Framework.Models.ItemUIStatus.New };
        }

        public async Task<Framework.Models.Response> Delete(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier id)
        {
            var response = await _customerApiClient.Delete(id);
            return response;
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
                            //SortFunc = (TableQuery<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> tableQuery, Framework.Models.QueryOrderDirections direction) =>
                            //{
                            //    tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                            //    return tableQuery;
                            //}
                        },
                        new Framework.MauiX.DataModels.ObservableQueryOrderBySetting
                        {
                            IsSelected = false,
                            DisplayName = AdventureWorksLT2019.Resx.Resources.UIStrings.ModifiedDate,
                            PropertyName = nameof(AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel.ModifiedDate),
                            Direction = Framework.Models.QueryOrderDirections.Descending, 
                            //FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                            //SortFunc = (TableQuery<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel> tableQuery, Framework.Models.QueryOrderDirections direction) =>
                            //{
                            //    tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                            //    return tableQuery;
                            //}
                        }
                    };
            return queryOrderBySettings;
        }
    }
}
