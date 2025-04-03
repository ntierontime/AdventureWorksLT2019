using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface ISalesOrderDetailRepository
    {

        Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query);

        Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> data);

        Task<Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> input);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel.DefaultView input, string[]? toUpdatePropertyList = null);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Get(SalesOrderDetailIdentifier id);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Create(SalesOrderDetailDataModel.DefaultView input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            SalesOrderDetailAdvancedQuery query);
    }
}

