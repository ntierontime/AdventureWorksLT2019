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
    public class CustomerService
        : ICustomerService
    {
        private readonly ICustomerRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(
            ICustomerRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<CustomerService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<CustomerDataModel[]>> Search(
            CustomerAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<CustomerCompositeModel> GetCompositeModel(CustomerIdentifier id, CustomerCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new CustomerCompositeModel();
                failedResponse.Responses.Add(CustomerCompositeModel.__DataOptions__.__Master__, new Response { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new CustomerCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<CustomerCompositeModel.__DataOptions__, Response>();
            responses.TryAdd(CustomerCompositeModel.__DataOptions__.__Master__, new Response { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(CustomerCompositeModel.__DataOptions__.CustomerAddresses_Via_CustomerID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _customerAddressRepository = scope.ServiceProvider.GetRequiredService<ICustomerAddressRepository>();
                        var query = new CustomerAddressAdvancedQuery { CustomerID = id.CustomerID, PageIndex = 1, PageSize = 5, OrderBys="ModifiedDate~DESC" };
                        var response = await _customerAddressRepository.Search(query);
                        responses.TryAdd(CustomerCompositeModel.__DataOptions__.CustomerAddresses_Via_CustomerID, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.CustomerAddresses_Via_CustomerID = response.ResponseBody;
                        }
                    }
                }));
            }

            if (dataOptions == null || dataOptions.Contains(CustomerCompositeModel.__DataOptions__.SalesOrderHeaders_Via_CustomerID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _salesOrderHeaderRepository = scope.ServiceProvider.GetRequiredService<ISalesOrderHeaderRepository>();
                        var query = new SalesOrderHeaderAdvancedQuery { CustomerID = id.CustomerID, PageIndex = 1, PageSize = 5, OrderBys="OrderDate~DESC" };
                        var response = await _salesOrderHeaderRepository.Search(query);
                        responses.TryAdd(CustomerCompositeModel.__DataOptions__.SalesOrderHeaders_Via_CustomerID, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.SalesOrderHeaders_Via_CustomerID = response.ResponseBody;
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
            successResponse.Responses = new Dictionary<CustomerCompositeModel.__DataOptions__, Response>(responses);
            return successResponse;
        }

        public async Task<PagedResponse<CustomerDataModel[]>> BulkUpdate(BatchActionViewModel<CustomerIdentifier, CustomerDataModel> data)
        {
            return await _thisRepository.BulkUpdate(data);
        }

        public async Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<CustomerDataModel>> Get(CustomerIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<CustomerDataModel>> Create(CustomerDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public CustomerDataModel GetDefault()
        {
            // TODO: please set default value here
            return new CustomerDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }
    }
}

