using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ISalesOrderDetailService
    {

        Task<PagedResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query);

        Task<SalesOrderDetailCompositeModel> GetCompositeModel(
            SalesOrderDetailIdentifier id, SalesOrderDetailCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<SalesOrderDetailIdentifier> ids);

        Task<Response<MultiItemsCUDModel<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> input);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel input);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Get(SalesOrderDetailIdentifier id);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Create(SalesOrderDetailDataModel input);
        SalesOrderDetailDataModel.DefaultView GetDefault();

        Task<Response> Delete(SalesOrderDetailIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            SalesOrderDetailAdvancedQuery query);
    }
}

