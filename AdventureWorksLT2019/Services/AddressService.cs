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

        public async Task<ListResponse<AddressDataModel[]>> Search(
            AddressAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<AddressCompositeModel> GetCompositeModel(
            AddressIdentifier id,
            Dictionary<AddressCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            AddressCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new AddressCompositeModel();
                failedResponse.Responses.Add(AddressCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new AddressCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<AddressCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(AddressCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(AddressCompositeModel.__DataOptions__.CustomerAddresses_Via_AddressID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _customerAddressRepository = scope.ServiceProvider.GetRequiredService<ICustomerAddressRepository>();
                        var query = new CustomerAddressAdvancedQuery
                        {
                            AddressID = id.AddressID,
                            PageIndex = 1,
                            PageSize = listItemRequest[AddressCompositeModel.__DataOptions__.CustomerAddresses_Via_AddressID].PageSize,
                            OrderBys= listItemRequest[AddressCompositeModel.__DataOptions__.CustomerAddresses_Via_AddressID].OrderBys,
                            PaginationOption = listItemRequest[AddressCompositeModel.__DataOptions__.CustomerAddresses_Via_AddressID].PaginationOption,
                        };
                        var response = await _customerAddressRepository.Search(query);
                        responses.TryAdd(AddressCompositeModel.__DataOptions__.CustomerAddresses_Via_AddressID, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
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
                        var query = new SalesOrderHeaderAdvancedQuery
                        {
                            BillToAddressID = id.AddressID,
                            PageIndex = 1,
                            PageSize = listItemRequest[AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_BillToAddressID].PageSize,
                            OrderBys= listItemRequest[AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_BillToAddressID].OrderBys,
                            PaginationOption = listItemRequest[AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_BillToAddressID].PaginationOption,
                        };
                        var response = await _salesOrderHeaderRepository.Search(query);
                        responses.TryAdd(AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_BillToAddressID, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
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
                        var query = new SalesOrderHeaderAdvancedQuery
                        {
                            ShipToAddressID = id.AddressID,
                            PageIndex = 1,
                            PageSize = listItemRequest[AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_ShipToAddressID].PageSize,
                            OrderBys= listItemRequest[AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_ShipToAddressID].OrderBys,
                            PaginationOption = listItemRequest[AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_ShipToAddressID].PaginationOption,
                        };
                        var response = await _salesOrderHeaderRepository.Search(query);
                        responses.TryAdd(AddressCompositeModel.__DataOptions__.SalesOrderHeaders_Via_ShipToAddressID, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
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
            successResponse.Responses = new Dictionary<AddressCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
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

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            AddressAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

