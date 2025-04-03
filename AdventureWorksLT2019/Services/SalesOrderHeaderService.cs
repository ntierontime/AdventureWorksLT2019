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
    public class SalesOrderHeaderService
        : ISalesOrderHeaderService
    {
        private readonly ISalesOrderHeaderRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SalesOrderHeaderService> _logger;

        public SalesOrderHeaderService(
            ISalesOrderHeaderRepository thisRepository,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<SalesOrderHeaderService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>> Search(
            SalesOrderHeaderAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null)
        {
            var response = await _thisRepository.Update(id, input, toUpdatePropertyList);
            return response;
        }

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Get(SalesOrderHeaderIdentifier id, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Get(id);
            return response;
        }

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Create(SalesOrderHeaderDataModel.DefaultView input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Create(input);
            return response;
        }

        public SalesOrderHeaderDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new SalesOrderHeaderDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            SalesOrderHeaderAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

