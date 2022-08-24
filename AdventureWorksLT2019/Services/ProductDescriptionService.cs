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
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ProductDescriptionService> _logger;

        public ProductDescriptionService(
            IProductDescriptionRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ProductDescriptionService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<ProductDescriptionDataModel[]>> Search(
            ProductDescriptionAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ProductDescriptionDataModel>> Update(ProductDescriptionIdentifier id, ProductDescriptionDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ProductDescriptionDataModel>> Get(ProductDescriptionIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ProductDescriptionDataModel>> Create(ProductDescriptionDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ProductDescriptionDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ProductDescriptionDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ProductDescriptionIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ProductDescriptionAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

