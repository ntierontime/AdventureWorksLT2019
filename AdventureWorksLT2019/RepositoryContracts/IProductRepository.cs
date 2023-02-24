using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IProductRepository
    {

        Task<ListResponse<ProductDataModel.DefaultView[]>> Search(
            ProductAdvancedQuery query);

        Task<Response> BulkDelete(List<ProductIdentifier> ids);

        Task<Response<MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductIdentifier, ProductDataModel.DefaultView> input);

        Task<Response<ProductDataModel.DefaultView>> Update(ProductIdentifier id, ProductDataModel input);

        Task<Response<ProductDataModel.DefaultView>> Get(ProductIdentifier id);

        Task<Response<ProductDataModel.DefaultView>> Create(ProductDataModel input);

        Task<Response> Delete(ProductIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductAdvancedQuery query);

        Task<Response<ProductDataModel.DefaultView>> CreateComposite(ProductCompositeModel input);
    }
}

