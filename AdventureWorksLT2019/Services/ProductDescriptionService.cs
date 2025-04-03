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
    public class ProductDescriptionService
        : IProductDescriptionService
    {
        private readonly IProductDescriptionRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ProductDescriptionService> _logger;

        public ProductDescriptionService(
            IProductDescriptionRepository thisRepository,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<ProductDescriptionService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<ListResponse<ProductDescriptionDataModel[]>> Search(
            ProductDescriptionAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ProductDescriptionDataModel>> Update(ProductDescriptionIdentifier id, ProductDescriptionDataModel input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null)
        {
            var response = await _thisRepository.Update(id, input, toUpdatePropertyList);
            return response;
        }

        public async Task<Response<ProductDescriptionDataModel>> Get(ProductDescriptionIdentifier id, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Get(id);
            return response;
        }

        public async Task<Response<ProductDescriptionDataModel>> Create(ProductDescriptionDataModel input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Create(input);
            return response;
        }

        public ProductDescriptionDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ProductDescriptionDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductDescriptionAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

