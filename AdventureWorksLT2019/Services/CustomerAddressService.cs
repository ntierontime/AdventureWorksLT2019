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
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<CustomerAddressService> _logger;

        public CustomerAddressService(
            ICustomerAddressRepository thisRepository,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<CustomerAddressService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<ListResponse<CustomerAddressDataModel.DefaultView[]>> Search(
            CustomerAddressAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ListResponse<CustomerAddressDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> data, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.BulkUpdate(data);
            return response;
        }

        public async Task<Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.MultiItemsCUD(input);
            return response;
        }

        public async Task<Response<CustomerAddressDataModel.DefaultView>> Update(CustomerAddressIdentifier id, CustomerAddressDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null)
        {
            var response = await _thisRepository.Update(id, input, toUpdatePropertyList);
            return response;
        }

        public async Task<Response<CustomerAddressDataModel.DefaultView>> Get(CustomerAddressIdentifier id, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Get(id);
            return response;
        }

        public async Task<Response<CustomerAddressDataModel.DefaultView>> Create(CustomerAddressDataModel.DefaultView input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Create(input);
            return response;
        }

        public CustomerAddressDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new CustomerAddressDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAddressAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

