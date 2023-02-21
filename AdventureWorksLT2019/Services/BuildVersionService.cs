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
    public class BuildVersionService
        : IBuildVersionService
    {
        private readonly IBuildVersionRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<BuildVersionService> _logger;

        public BuildVersionService(
            IBuildVersionRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<BuildVersionService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<BuildVersionDataModel[]>> Search(
            BuildVersionAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<BuildVersionDataModel>> Update(BuildVersionIdentifier id, BuildVersionDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<BuildVersionDataModel>> Create(BuildVersionDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public BuildVersionDataModel GetDefault()
        {
            // TODO: please set default value here
            return new BuildVersionDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            BuildVersionAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

