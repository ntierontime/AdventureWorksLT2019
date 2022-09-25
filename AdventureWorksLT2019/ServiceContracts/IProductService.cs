using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductService
    {

        Task<ListResponse<ProductDataModel.DefaultView[]>> Search(
            ProductAdvancedQuery query);

        Task<ProductCompositeModel> GetCompositeModel(
            ProductIdentifier id,
            Dictionary<ProductCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ProductCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response<ProductDataModel.DefaultView>> Update(ProductIdentifier id, ProductDataModel input);

        Task<Response<ProductDataModel.DefaultView>> Get(ProductIdentifier id);

        Task<Response<ProductDataModel.DefaultView>> Create(ProductDataModel input);
        ProductDataModel.DefaultView GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductAdvancedQuery query);
    }
}

