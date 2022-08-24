using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductService
    {

        Task<PagedResponse<ProductDataModel.DefaultView[]>> Search(
            ProductAdvancedQuery query);

        Task<ProductCompositeModel> GetCompositeModel(
            ProductIdentifier id, ProductCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response<ProductDataModel.DefaultView>> Update(ProductIdentifier id, ProductDataModel input);

        Task<Response<ProductDataModel.DefaultView>> Get(ProductIdentifier id);

        Task<Response<ProductDataModel.DefaultView>> Create(ProductDataModel input);
        ProductDataModel.DefaultView GetDefault();
    }
}

