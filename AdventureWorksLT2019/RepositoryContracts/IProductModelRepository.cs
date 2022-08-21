using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IProductModelRepository
    {

        Task<PagedResponse<ProductModelDataModel[]>> Search(
            ProductModelAdvancedQuery query);

        Task<Response> BulkDelete(List<ProductModelIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ProductModelIdentifier, ProductModelDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ProductModelIdentifier, ProductModelDataModel> input);

        Task<Response<ProductModelDataModel>> Update(ProductModelIdentifier id, ProductModelDataModel input);

        Task<Response<ProductModelDataModel>> Get(ProductModelIdentifier id);

        Task<Response<ProductModelDataModel>> Create(ProductModelDataModel input);

        Task<Response> Delete(ProductModelIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ProductModelAdvancedQuery query);
    }
}

