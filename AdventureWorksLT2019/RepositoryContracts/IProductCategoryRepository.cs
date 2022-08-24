using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IProductCategoryRepository
    {

        Task<PagedResponse<ProductCategoryDataModel.DefaultView[]>> Search(
            ProductCategoryAdvancedQuery query);

        Task<Response<ProductCategoryDataModel.DefaultView>> Update(ProductCategoryIdentifier id, ProductCategoryDataModel input);

        Task<Response<ProductCategoryDataModel.DefaultView>> Get(ProductCategoryIdentifier id);

        Task<Response<ProductCategoryDataModel.DefaultView>> Create(ProductCategoryDataModel input);

        Task<Response> Delete(ProductCategoryIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ProductCategoryAdvancedQuery query);
    }
}

