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
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<BuildVersionService> _logger;

        public BuildVersionService(
            IBuildVersionRepository thisRepository,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<BuildVersionService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<ListResponse<BuildVersionDataModel[]>> Search(
            BuildVersionAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ListResponse<BuildVersionDataModel[]>> BulkUpdate(BatchActionRequest<BuildVersionIdentifier, BuildVersionDataModel> data, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.BulkUpdate(data);
            return response;
        }

        public async Task<Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel> input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.MultiItemsCUD(input);
            return response;
        }

        public async Task<Response<BuildVersionDataModel>> Update(BuildVersionIdentifier id, BuildVersionDataModel input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null)
        {
            var response = await _thisRepository.Update(id, input, toUpdatePropertyList);
            return response;
        }

        public async Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Get(id);
            return response;
        }

        public async Task<Response<BuildVersionDataModel>> Create(BuildVersionDataModel input, ClaimsModel? claimsModel)
        {
            var response = await _thisRepository.Create(input);
            return response;
        }

        public BuildVersionDataModel GetDefault()
        {
            // TODO: please set default value here
            return new BuildVersionDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            BuildVersionAdvancedQuery query, ClaimsModel? claimsModel)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

