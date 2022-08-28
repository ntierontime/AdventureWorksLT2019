using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IErrorLogRepository
    {

        Task<PagedResponse<ErrorLogDataModel[]>> Search(
            ErrorLogAdvancedQuery query);

        Task<Response> BulkDelete(List<ErrorLogIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ErrorLogIdentifier, ErrorLogDataModel> input);

        Task<Response<ErrorLogDataModel>> Update(ErrorLogIdentifier id, ErrorLogDataModel input);

        Task<Response<ErrorLogDataModel>> Get(ErrorLogIdentifier id);

        Task<Response<ErrorLogDataModel>> Create(ErrorLogDataModel input);

        Task<Response> Delete(ErrorLogIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ErrorLogAdvancedQuery query);
    }
}

