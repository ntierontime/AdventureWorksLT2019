using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IErrorLogService
    {

        Task<ListResponse<ErrorLogDataModel[]>> Search(
            ErrorLogAdvancedQuery query);

        Task<ErrorLogCompositeModel> GetCompositeModel(
            ErrorLogIdentifier id,
            Dictionary<ErrorLogCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ErrorLogCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ErrorLogIdentifier> ids);

        Task<Response<MultiItemsCUDRequest<ErrorLogIdentifier, ErrorLogDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<ErrorLogIdentifier, ErrorLogDataModel> input);

        Task<Response<ErrorLogDataModel>> Update(ErrorLogIdentifier id, ErrorLogDataModel input);

        Task<Response<ErrorLogDataModel>> Get(ErrorLogIdentifier id);

        Task<Response<ErrorLogDataModel>> Create(ErrorLogDataModel input);
        ErrorLogDataModel GetDefault();

        Task<Response> Delete(ErrorLogIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ErrorLogAdvancedQuery query);

        Task<Response<ErrorLogDataModel>> CreateComposite(ErrorLogCompositeModel input);
    }
}

