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
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<AddressService> _logger;

        public AddressService(
            IAddressRepository thisRepository,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<AddressService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<ListResponse<AddressDataModel[]>> Search(
            AddressAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<AddressDataModel>> Update(AddressIdentifier id, AddressDataModel input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null)
        {
            var response = await _thisRepository.Update(id, input, toUpdatePropertyList);
            return response;
        }

        public async Task<Response<AddressDataModel>> Get(AddressIdentifier id, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Get(id);
            return response;
        }

        public async Task<Response<AddressDataModel>> Create(AddressDataModel input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Create(input);
            return response;
        }

        public AddressDataModel GetDefault()
        {
            // TODO: please set default value here
            return new AddressDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            AddressAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

