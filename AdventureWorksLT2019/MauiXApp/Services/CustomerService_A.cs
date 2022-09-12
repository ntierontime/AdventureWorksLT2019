namespace AdventureWorksLT2019.MauiXApp.Services
{
    public class CustomerService_A
    {
        private readonly AdventureWorksLT2019.MauiXApp.WebApiClients.CustomerApiClient _customerApiClient;
        private readonly AdventureWorksLT2019.MauiXApp.SQLite.CustomerRepository _customerRepository;
        public CustomerService_A(
            AdventureWorksLT2019.MauiXApp.WebApiClients.CustomerApiClient customerApiClient,
            AdventureWorksLT2019.MauiXApp.SQLite.CustomerRepository customerRepository)
        {
            _customerApiClient = customerApiClient;
            _customerRepository = customerRepository;
        }

        public async Task CacheAllData()
        {
            var result = await Search(new AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery());
            await _customerRepository.Save(result.ResponseBody);
        }

        public async Task<Framework.Models.ListResponse<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel[]>> Search(
            AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery query)
        {
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
    }
}
