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
    public class ProductModelService
        : IProductModelService
    {
        private readonly IProductModelRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ProductModelService> _logger;

        public ProductModelService(
            IProductModelRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ProductModelService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ProductModelDataModel[]>> Search(
            ProductModelAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ProductModelDataModel>> Update(ProductModelIdentifier id, ProductModelDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ProductModelDataModel>> Get(ProductModelIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ProductModelDataModel>> Create(ProductModelDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ProductModelDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ProductModelDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ProductModelIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

