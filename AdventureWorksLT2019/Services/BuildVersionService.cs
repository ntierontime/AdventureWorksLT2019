using AdventureWorksLT2019.RepositoryContracts;
using AdventureWorksLT2019.ServiceContracts;
using AdventureWorksLT2019.Models;
using Framework.Models;
using Framework.Common;
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

        public async Task<BuildVersionCompositeModel> GetCompositeModel(
            BuildVersionIdentifier id,
            Dictionary<BuildVersionCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            BuildVersionCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new BuildVersionCompositeModel();
                failedResponse.Responses.Add(BuildVersionCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new BuildVersionCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<BuildVersionCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(BuildVersionCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }
            }
            successResponse.Responses = new Dictionary<BuildVersionCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<BuildVersionIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
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

        public async Task<Response> Delete(BuildVersionIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            BuildVersionAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

