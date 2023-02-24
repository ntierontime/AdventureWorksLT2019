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

        public async Task<ErrorLogCompositeModel> GetCompositeModel(
            ErrorLogIdentifier id,
            Dictionary<ErrorLogCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ErrorLogCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ErrorLogCompositeModel();
                failedResponse.Responses.Add(ErrorLogCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ErrorLogCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ErrorLogCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(ErrorLogCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

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
            successResponse.Responses = new Dictionary<ErrorLogCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ErrorLogIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDRequest<ErrorLogIdentifier, ErrorLogDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<ErrorLogIdentifier, ErrorLogDataModel> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
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

        public async Task<Response> Delete(ErrorLogIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ErrorLogAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }

        public async Task<Response<ErrorLogDataModel>> CreateComposite(ErrorLogCompositeModel input)
        {
            return await _thisRepository.CreateComposite(input);
        }
    }
}

