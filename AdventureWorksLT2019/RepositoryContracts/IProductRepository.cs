using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IProductRepository
    {

        Task<PagedResponse<ProductDataModel.DefaultView[]>> Search(
            ProductAdvancedQuery query);

        Task<Response> BulkDelete(List<ProductIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ProductIdentifier, ProductDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<ProductIdentifier, ProductDataModel.DefaultView> input);

        Task<Response<ProductDataModel.DefaultView>> Update(ProductIdentifier id, ProductDataModel input);

        Task<Response<ProductDataModel.DefaultView>> Get(ProductIdentifier id);

        Task<Response<ProductDataModel.DefaultView>> Create(ProductDataModel input);

        Task<Response> Delete(ProductIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ProductAdvancedQuery query);
    }
}

