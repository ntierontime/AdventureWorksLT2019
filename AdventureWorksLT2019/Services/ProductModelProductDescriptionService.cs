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
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ProductModelProductDescriptionService> _logger;

        public ProductModelProductDescriptionService(
            IProductModelProductDescriptionRepository thisRepository,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<ProductModelProductDescriptionService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> Search(
            ProductModelProductDescriptionAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> data, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.BulkUpdate(data);
            return response;
        }

        public async Task<Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.MultiItemsCUD(input);
            return response;
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Update(ProductModelProductDescriptionIdentifier id, ProductModelProductDescriptionDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null)
        {
            var response = await _thisRepository.Update(id, input, toUpdatePropertyList);
            return response;
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Get(ProductModelProductDescriptionIdentifier id, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Get(id);
            return response;
        }

        public async Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Create(ProductModelProductDescriptionDataModel.DefaultView input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Create(input);
            return response;
        }

        public ProductModelProductDescriptionDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new ProductModelProductDescriptionDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelProductDescriptionAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

