using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ISalesOrderHeaderService
    {

        Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>> Search(
            SalesOrderHeaderAdvancedQuery query);

        Task<SalesOrderHeaderCompositeModel> GetCompositeModel(
            SalesOrderHeaderIdentifier id,
            Dictionary<SalesOrderHeaderCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            SalesOrderHeaderCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<SalesOrderHeaderIdentifier> ids);

        Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> data);

        Task<Response<MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> input);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel input);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Get(SalesOrderHeaderIdentifier id);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Create(SalesOrderHeaderDataModel input);
        SalesOrderHeaderDataModel.DefaultView GetDefault();

        Task<Response> Delete(SalesOrderHeaderIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            SalesOrderHeaderAdvancedQuery query);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> CreateComposite(SalesOrderHeaderCompositeModel input);
    }
}

