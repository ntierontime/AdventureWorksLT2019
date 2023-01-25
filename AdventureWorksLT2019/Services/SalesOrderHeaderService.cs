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

        public async Task<SalesOrderHeaderCompositeModel> GetCompositeModel(
            SalesOrderHeaderIdentifier id,
            Dictionary<SalesOrderHeaderCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            SalesOrderHeaderCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new SalesOrderHeaderCompositeModel();
                failedResponse.Responses.Add(SalesOrderHeaderCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new SalesOrderHeaderCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<SalesOrderHeaderCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(SalesOrderHeaderCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(SalesOrderHeaderCompositeModel.__DataOptions__.SalesOrderDetails_Via_SalesOrderID))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _salesOrderDetailRepository = scope.ServiceProvider.GetRequiredService<ISalesOrderDetailRepository>();
                        var query = new SalesOrderDetailAdvancedQuery
                        {
                            SalesOrderID = id.SalesOrderID,
                            PageIndex = 1,
                            PageSize = listItemRequest[SalesOrderHeaderCompositeModel.__DataOptions__.SalesOrderDetails_Via_SalesOrderID].PageSize,
                            OrderBys= listItemRequest[SalesOrderHeaderCompositeModel.__DataOptions__.SalesOrderDetails_Via_SalesOrderID].OrderBys,
                            PaginationOption = listItemRequest[SalesOrderHeaderCompositeModel.__DataOptions__.SalesOrderDetails_Via_SalesOrderID].PaginationOption,
                        };
                        var response = await _salesOrderDetailRepository.Search(query);
                        responses.TryAdd(SalesOrderHeaderCompositeModel.__DataOptions__.SalesOrderDetails_Via_SalesOrderID, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.SalesOrderDetails_Via_SalesOrderID = response.ResponseBody;
                        }
                    }
                }));
            }

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }
            }
            successResponse.Responses = new Dictionary<SalesOrderHeaderCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<SalesOrderHeaderIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> data)
        {
            return await _thisRepository.BulkUpdate(data);
        }

        public async Task<Response<MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
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

        public async Task<Response> Delete(SalesOrderHeaderIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            SalesOrderHeaderAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}

