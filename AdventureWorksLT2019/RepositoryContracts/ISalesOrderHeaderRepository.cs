using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface ISalesOrderHeaderRepository
    {

        Task<ListResponse<SalesOrderHeaderDataModel.DefaultView[]>> Search(
            SalesOrderHeaderAdvancedQuery query);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Update(SalesOrderHeaderIdentifier id, SalesOrderHeaderDataModel.DefaultView input, string[]? toUpdatePropertyList = null);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Get(SalesOrderHeaderIdentifier id);

        Task<Response<SalesOrderHeaderDataModel.DefaultView>> Create(SalesOrderHeaderDataModel.DefaultView input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            SalesOrderHeaderAdvancedQuery query);
    }
}

