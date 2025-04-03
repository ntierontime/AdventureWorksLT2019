using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ISalesOrderHeaderService
    {

        Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>> Search(
            SalesOrderHeaderAdvancedQuery query, ClaimsModel? claimsModel);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Get(SalesOrderHeaderIdentifier id, ClaimsModel? claimsModel);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Create(SalesOrderHeaderDataModel.DefaultView input, ClaimsModel? claimsModel);
        SalesOrderHeaderDataModel.DefaultView GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            SalesOrderHeaderAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

