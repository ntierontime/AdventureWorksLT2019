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
    public class ErrorLogService
        : IErrorLogService
    {
        private readonly IErrorLogRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ErrorLogService> _logger;

        public ErrorLogService(
            IErrorLogRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ErrorLogService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ErrorLogDataModel[]>> Search(
            ErrorLogAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ErrorLogDataModel>> Update(ErrorLogIdentifier id, ErrorLogDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ErrorLogDataModel>> Get(ErrorLogIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ErrorLogDataModel>> Create(ErrorLogDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ErrorLogDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ErrorLogDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }
    }
}

