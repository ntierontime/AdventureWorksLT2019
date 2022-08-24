using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface ISalesOrderHeaderRepository
    {

        Task<PagedResponse<SalesOrderHeaderDataModel.DefaultView[]>> Search(
            SalesOrderHeaderAdvancedQuery query);

        Task<PagedResponse<SalesOrderHeaderDataModel.DefaultView[]>> BulkUpdate(BatchActionViewModel<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> data);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel input);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Get(SalesOrderHeaderIdentifier id);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Create(SalesOrderHeaderDataModel input);
    }
}

