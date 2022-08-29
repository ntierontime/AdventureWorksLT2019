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
    }
}

