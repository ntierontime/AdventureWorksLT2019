using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IErrorLogService
    {

        Task<PagedResponse<ErrorLogDataModel[]>> Search(
            ErrorLogAdvancedQuery query);

        Task<ErrorLogCompositeModel> GetCompositeModel(
            ErrorLogIdentifier id, ErrorLogCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ErrorLogIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel> input);

        Task<Response<ErrorLogDataModel>> Update(ErrorLogIdentifier id, ErrorLogDataModel input);

        Task<Response<ErrorLogDataModel>> Get(ErrorLogIdentifier id);

        Task<Response<ErrorLogDataModel>> Create(ErrorLogDataModel input);
        ErrorLogDataModel GetDefault();

        Task<Response> Delete(ErrorLogIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ErrorLogAdvancedQuery query);
    }
}

