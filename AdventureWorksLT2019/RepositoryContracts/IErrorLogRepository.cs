using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IErrorLogRepository
    {

        Task<PagedResponse<ErrorLogDataModel[]>> Search(
            ErrorLogAdvancedQuery query);

        Task<Response<ErrorLogDataModel>> Update(ErrorLogIdentifier id, ErrorLogDataModel input);

        Task<Response<ErrorLogDataModel>> Get(ErrorLogIdentifier id);

        Task<Response<ErrorLogDataModel>> Create(ErrorLogDataModel input);
    }
}

