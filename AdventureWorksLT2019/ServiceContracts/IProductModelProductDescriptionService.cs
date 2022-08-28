using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductModelProductDescriptionService
    {

        Task<PagedResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> Search(
            ProductModelProductDescriptionAdvancedQuery query);

        Task<ProductModelProductDescriptionCompositeModel> GetCompositeModel(
            ProductModelProductDescriptionIdentifier id,
            Dictionary<ProductModelProductDescriptionCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ProductModelProductDescriptionCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ProductModelProductDescriptionIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> input);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Update(ProductModelProductDescriptionIdentifier id, ProductModelProductDescriptionDataModel input);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Get(ProductModelProductDescriptionIdentifier id);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Create(ProductModelProductDescriptionDataModel input);
        ProductModelProductDescriptionDataModel.DefaultView GetDefault();

        Task<Response> Delete(ProductModelProductDescriptionIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ProductModelProductDescriptionAdvancedQuery query);
    }
}

