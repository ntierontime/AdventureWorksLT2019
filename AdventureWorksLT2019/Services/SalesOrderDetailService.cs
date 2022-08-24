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
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<SalesOrderDetailService> _logger;

        public SalesOrderDetailService(
            ISalesOrderDetailRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<SalesOrderDetailService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<SalesOrderDetailDataModel.DefaultView>> Get(SalesOrderDetailIdentifier id)
        {
            return await _thisRepository.Get(id);
        }
    }
}

