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
    public class SalesOrderDetailService
        : ISalesOrderDetailService
    {
        private readonly ISalesOrderDetailRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SalesOrderDetailService> _logger;

        public SalesOrderDetailService(
            ISalesOrderDetailRepository thisRepository,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<SalesOrderDetailService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> data, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.BulkUpdate(data);
            return response;
        }

        public async Task<Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.MultiItemsCUD(input);
            return response;
        }

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null)
        {
            var response = await _thisRepository.Update(id, input, toUpdatePropertyList);
            return response;
        }

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Get(SalesOrderDetailIdentifier id, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Get(id);
            return response;
        }

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Create(SalesOrderDetailDataModel.DefaultView input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Create(input);
            return response;
        }

        public SalesOrderDetailDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new SalesOrderDetailDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            SalesOrderDetailAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

