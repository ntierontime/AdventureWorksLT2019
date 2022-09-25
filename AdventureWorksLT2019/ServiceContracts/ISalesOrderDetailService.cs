using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ISalesOrderDetailService
    {

        Task<ListResponse<SalesOrderDetailDataModel.DefaultView[]>> Search(
            SalesOrderDetailAdvancedQuery query);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Update(SalesOrderDetailIdentifier id, SalesOrderDetailDataModel input);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Get(SalesOrderDetailIdentifier id);

        Task<Response<SalesOrderDetailDataModel.DefaultView>> Create(SalesOrderDetailDataModel input);
        SalesOrderDetailDataModel.DefaultView GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            SalesOrderDetailAdvancedQuery query);
    }
}

