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
    public class CustomerAddressService
        : ICustomerAddressService
    {
        private readonly ICustomerAddressRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<CustomerAddressService> _logger;

        public CustomerAddressService(
            ICustomerAddressRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<CustomerAddressService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<CustomerAddressDataModel.DefaultView[]>> Search(
            CustomerAddressAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<CustomerAddressCompositeModel> GetCompositeModel(
            CustomerAddressIdentifier id,
            Dictionary<CustomerAddressCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            CustomerAddressCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new CustomerAddressCompositeModel();
                failedResponse.Responses.Add(CustomerAddressCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new CustomerAddressCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<CustomerAddressCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(CustomerAddressCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }
            }
            successResponse.Responses = new Dictionary<CustomerAddressCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<CustomerAddressIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<CustomerAddressDataModel.DefaultView>> Update(CustomerAddressIdentifier id, CustomerAddressDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<CustomerAddressDataModel.DefaultView>> Get(CustomerAddressIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<CustomerAddressDataModel.DefaultView>> Create(CustomerAddressDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public CustomerAddressDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new CustomerAddressDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(CustomerAddressIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAddressAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

