using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.ServiceContracts;
using AdventureWorksLT2019.Models;
using Framework.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Collections.Concurrent;

namespace AdventureWorksLT2019.Services
{
    public class AddressService
        : IAddressService
    {
        private readonly IAddressRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<AddressService> _logger;

        public AddressService(
            IAddressRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<AddressService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<AddressDataModel[]>> Search(
            AddressAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<AddressCompositeModel> GetCompositeModel(AddressIdentifier id, AddressCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new AddressCompositeModel();
                failedResponse.Responses.Add(AddressCompositeModel.__DataOptions__.__Master__, new Response { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new AddressCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<AddressCompositeModel.__DataOptions__, Response>();
            responses.TryAdd(AddressCompositeModel.__DataOptions__.__Master__, new Response { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(AddressCompositeModel.__DataOptions__.CustomerAddresses_Via_AddressID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _customerAddressRepository = scope.ServiceProvider.GetRequiredService<ICustomerAddressRepository>();
                        var query = new CustomerAddressAdvancedQuery { AddressID = id.AddressID, PageIndex = 1, PageSize = 5, OrderBys="ModifiedDate~DESC" };
                        var response = await _customerAddressRepository.Search(query);
                        responses.TryAdd(AddressCompositeModel.__DataOptions__.CustomerAddresses_Via_AddressID, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.CustomerAddresses_Via_AddressID = response.ResponseBody;
                        }
                    }
                }));
            }

            if (dataOptions == null || dataOptions.Contains(AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_BillToAddressID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _salesOrderHeaderRepository = scope.ServiceProvider.GetRequiredService<ISalesOrderHeaderRepository>();
                        var query = new SalesOrderHeaderAdvancedQuery { BillToAddressID = id.AddressID, PageIndex = 1, PageSize = 5, OrderBys="OrderDate~DESC" };
                        var response = await _salesOrderHeaderRepository.Search(query);
                        responses.TryAdd(AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_BillToAddressID, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.SalesOrderHeaders_Via_BillToAddressID = response.ResponseBody;
                        }
                    }
                }));
            }

            if (dataOptions == null || dataOptions.Contains(AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_ShipToAddressID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _salesOrderHeaderRepository = scope.ServiceProvider.GetRequiredService<ISalesOrderHeaderRepository>();
                        var query = new SalesOrderHeaderAdvancedQuery { ShipToAddressID = id.AddressID, PageIndex = 1, PageSize = 5, OrderBys="OrderDate~DESC" };
                        var response = await _salesOrderHeaderRepository.Search(query);
                        responses.TryAdd(AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_ShipToAddressID, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.SalesOrderHeaders_Via_ShipToAddressID = response.ResponseBody;
                        }
                    }
                }));
            }

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }
            }
            successResponse.Responses = new Dictionary<AddressCompositeModel.__DataOptions__, Response>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<AddressIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDModel<AddressIdentifier, AddressDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<AddressIdentifier, AddressDataModel> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<AddressDataModel>> Update(AddressIdentifier id, AddressDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<AddressDataModel>> Get(AddressIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<AddressDataModel>> Create(AddressDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public AddressDataModel GetDefault()
        {
            // TODO: please set default value here
            return new AddressDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(AddressIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            AddressAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

