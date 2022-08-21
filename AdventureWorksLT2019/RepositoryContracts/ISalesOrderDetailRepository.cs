using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface ISalesOrderDetailRepository
    {

        Task<PagedResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query);

        Task<Response> BulkDelete(List<SalesOrderDetailIdentifier> ids);

        Task<Response<MultiItemsCUDModel<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> input);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel input);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Get(SalesOrderDetailIdentifier id);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Create(SalesOrderDetailDataModel input);

        Task<Response> Delete(SalesOrderDetailIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            SalesOrderDetailAdvancedQuery query);
    }
}

