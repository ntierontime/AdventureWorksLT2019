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
    public class ProductCategoryService
        : IProductCategoryService
    {
        private readonly IProductCategoryRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ProductCategoryService> _logger;

        public ProductCategoryService(
            IProductCategoryRepository thisRepository,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<ProductCategoryService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<ListResponse<ProductCategoryDataModel.DefaultView[]>> Search(
            ProductCategoryAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Update(ProductCategoryIdentifier id, ProductCategoryDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null)
        {
            var response = await _thisRepository.Update(id, input, toUpdatePropertyList);
            return response;
        }

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Get(ProductCategoryIdentifier id, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Get(id);
            return response;
        }

        public async Task<Response<ProductCategoryDataModel.DefaultView>> Create(ProductCategoryDataModel.DefaultView input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Create(input);
            return response;
        }

        public ProductCategoryDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new ProductCategoryDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductCategoryAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

