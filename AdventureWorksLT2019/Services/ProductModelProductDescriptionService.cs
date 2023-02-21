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
    public class ProductModelProductDescriptionService
        : IProductModelProductDescriptionService
    {
        private readonly IProductModelProductDescriptionRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ProductModelProductDescriptionService> _logger;

        public ProductModelProductDescriptionService(
            IProductModelProductDescriptionRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ProductModelProductDescriptionService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> Search(
            ProductModelProductDescriptionAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Update(ProductModelProductDescriptionIdentifier id, ProductModelProductDescriptionDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Get(ProductModelProductDescriptionIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Create(ProductModelProductDescriptionDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ProductModelProductDescriptionDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new ProductModelProductDescriptionDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelProductDescriptionAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

