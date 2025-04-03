using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IProductCategoryRepository
    {

        Task<ListResponse<ProductCategoryDataModel.DefaultView[]>> Search(
            ProductCategoryAdvancedQuery query);

        Task<Response<ProductCategoryDataModel.DefaultView>> Update(ProductCategoryIdentifier id, ProductCategoryDataModel.DefaultView input, string[]? toUpdatePropertyList = null);

        Task<Response<ProductCategoryDataModel.DefaultView>> Get(ProductCategoryIdentifier id);

        Task<Response<ProductCategoryDataModel.DefaultView>> Create(ProductCategoryDataModel.DefaultView input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductCategoryAdvancedQuery query);
    }
}

