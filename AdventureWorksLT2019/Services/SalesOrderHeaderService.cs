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
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<SalesOrderHeaderService> _logger;

        public SalesOrderHeaderService(
            ISalesOrderHeaderRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<SalesOrderHeaderService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>> Search(
            SalesOrderHeaderAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> data)
        {
            return await _thisRepository.BulkUpdate(data);
        }

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Get(SalesOrderHeaderIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<SalesOrderHeaderDataModel.DefaultView>> Create(SalesOrderHeaderDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public SalesOrderHeaderDataModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new SalesOrderHeaderDataModel.DefaultView { ItemUIStatus______ = ItemUIStatus.New };
        }
    }
}

