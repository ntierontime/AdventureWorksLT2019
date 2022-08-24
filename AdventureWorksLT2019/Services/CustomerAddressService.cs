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

        public async Task<PagedResponse<CustomerAddressDataModel.DefaultView[]>> Search(
            CustomerAddressAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<CustomerAddressDataModel.DefaultView>> Get(CustomerAddressIdentifier id)
        {
            return await _thisRepository.Get(id);
        }
    }
}

