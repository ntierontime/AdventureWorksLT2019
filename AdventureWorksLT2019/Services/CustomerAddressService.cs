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

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAddressAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

