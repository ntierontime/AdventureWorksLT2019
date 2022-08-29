using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IErrorLogService
    {

        Task<ListResponse<ErrorLogDataModel[]>> Search(
            ErrorLogAdvancedQuery query);

        Task<Response<ErrorLogDataModel>> Update(ErrorLogIdentifier id, ErrorLogDataModel input);

        Task<Response<ErrorLogDataModel>> Get(ErrorLogIdentifier id);

        Task<Response<ErrorLogDataModel>> Create(ErrorLogDataModel input);
        ErrorLogDataModel GetDefault();
    }
}

