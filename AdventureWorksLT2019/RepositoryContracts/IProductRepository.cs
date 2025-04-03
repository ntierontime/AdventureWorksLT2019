using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IProductRepository
    {

        Task<ListResponse<ProductDataModel.DefaultView[]>> Search(
            ProductAdvancedQuery query);

        Task<Response<ProductDataModel.DefaultView>> Update(ProductIdentifier id, ProductDataModel.DefaultView input, string[]? toUpdatePropertyList = null);

        Task<Response<ProductDataModel.DefaultView>> Get(ProductIdentifier id);

        Task<Response<ProductDataModel.DefaultView>> Create(ProductDataModel.DefaultView input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductAdvancedQuery query);
    }
}

