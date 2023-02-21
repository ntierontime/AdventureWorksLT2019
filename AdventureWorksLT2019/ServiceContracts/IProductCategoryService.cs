using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductCategoryService
    {

        Task<ListResponse<ProductCategoryDataModel.DefaultView[]>> Search(
            ProductCategoryAdvancedQuery query);

        Task<ProductCategoryCompositeModel> GetCompositeModel(
            ProductCategoryIdentifier id,
            Dictionary<ProductCategoryCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ProductCategoryCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response<ProductCategoryDataModel.DefaultView>> Update(ProductCategoryIdentifier id, ProductCategoryDataModel input);

        Task<Response<ProductCategoryDataModel.DefaultView>> Get(ProductCategoryIdentifier id);

        Task<Response<ProductCategoryDataModel.DefaultView>> Create(ProductCategoryDataModel input);
        ProductCategoryDataModel.DefaultView GetDefault();

        Task<Response> Delete(ProductCategoryIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductCategoryAdvancedQuery query);
    }
}

