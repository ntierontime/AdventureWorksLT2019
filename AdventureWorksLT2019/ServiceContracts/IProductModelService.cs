using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductModelService
    {

        Task<ListResponse<ProductModelDataModel[]>> Search(
            ProductModelAdvancedQuery query);

        Task<ProductModelCompositeModel> GetCompositeModel(
            ProductModelIdentifier id,
            Dictionary<ProductModelCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ProductModelCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response<ProductModelDataModel>> Update(ProductModelIdentifier id, ProductModelDataModel input);

        Task<Response<ProductModelDataModel>> Get(ProductModelIdentifier id);

        Task<Response<ProductModelDataModel>> Create(ProductModelDataModel input);
        ProductModelDataModel GetDefault();

        Task<Response> Delete(ProductModelIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelAdvancedQuery query);
    }
}

