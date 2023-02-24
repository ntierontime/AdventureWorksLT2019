using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ISalesOrderDetailService
    {

        Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query);

        Task<SalesOrderDetailCompositeModel> GetCompositeModel(
            SalesOrderDetailIdentifier id,
            Dictionary<SalesOrderDetailCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            SalesOrderDetailCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<SalesOrderDetailIdentifier> ids);

        Task<Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> input);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel input);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Get(SalesOrderDetailIdentifier id);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Create(SalesOrderDetailDataModel input);
        SalesOrderDetailDataModel.DefaultView GetDefault();

        Task<Response> Delete(SalesOrderDetailIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            SalesOrderDetailAdvancedQuery query);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> CreateComposite(SalesOrderDetailCompositeModel input);
    }
}

