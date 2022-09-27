using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductDescriptionService
    {

        Task<ListResponse<ProductDescriptionDataModel[]>> Search(
            ProductDescriptionAdvancedQuery query);

        Task<ProductDescriptionCompositeModel> GetCompositeModel(
            ProductDescriptionIdentifier id,
            Dictionary<ProductDescriptionCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ProductDescriptionCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ProductDescriptionIdentifier> ids);

        Task<Response<MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductDescriptionIdentifier, ProductDescriptionDataModel> input);

        Task<Response<ProductDescriptionDataModel>> Update(ProductDescriptionIdentifier id, ProductDescriptionDataModel input);

        Task<Response<ProductDescriptionDataModel>> Get(ProductDescriptionIdentifier id);

        Task<Response<ProductDescriptionDataModel>> Create(ProductDescriptionDataModel input);
        ProductDescriptionDataModel GetDefault();

        Task<Response> Delete(ProductDescriptionIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductDescriptionAdvancedQuery query);
    }
}

