using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ISalesOrderDetailService
    {

        Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query, ClaimsModel? claimsModel);

        Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> data, ClaimsModel? claimsModel);

        Task<Response<MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<SalesOrderDetailIdentifier, SalesOrderDetailDataModel.DefaultView> input, ClaimsModel? claimsModel);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Get(SalesOrderDetailIdentifier id, ClaimsModel? claimsModel);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Create(SalesOrderDetailDataModel.DefaultView input, ClaimsModel? claimsModel);
        SalesOrderDetailDataModel.DefaultView GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            SalesOrderDetailAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

