using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ISalesOrderHeaderService
    {

        Task<PagedResponse<SalesOrderHeaderDataModel.DefaultView[]>> Search(
            SalesOrderHeaderAdvancedQuery query);

        Task<SalesOrderHeaderCompositeModel> GetCompositeModel(
            SalesOrderHeaderIdentifier id, SalesOrderHeaderCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<SalesOrderHeaderIdentifier> ids);

        Task<PagedResponse<SalesOrderHeaderDataModel.DefaultView[]>> BulkUpdate(BatchActionViewModel<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> data);

        Task<Response<MultiItemsCUDModel<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<SalesOrderHeaderIdentifier, SalesOrderHeaderDataModel.DefaultView> input);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel input);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Get(SalesOrderHeaderIdentifier id);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Create(SalesOrderHeaderDataModel input);
        SalesOrderHeaderDataModel.DefaultView GetDefault();

        Task<Response> Delete(SalesOrderHeaderIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            SalesOrderHeaderAdvancedQuery query);
    }
}

