using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IProductModelRepository
    {

        Task<ListResponse<ProductModelDataModel[]>> Search(
            ProductModelAdvancedQuery query);

        Task<Response<ProductModelDataModel>> Update(ProductModelIdentifier id, ProductModelDataModel input, string[]? toUpdatePropertyList = null);

        Task<Response<ProductModelDataModel>> Get(ProductModelIdentifier id);

        Task<Response<ProductModelDataModel>> Create(ProductModelDataModel input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelAdvancedQuery query);
    }
}

