using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductCategoryService
    {

        Task<PagedResponse<ProductCategoryDataModel.DefaultView[]>> Search(
            ProductCategoryAdvancedQuery query);

        Task<ProductCategoryCompositeModel> GetCompositeModel(
            ProductCategoryIdentifier id, ProductCategoryCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ProductCategoryIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<ProductCategoryIdentifier, ProductCategoryDataModel.DefaultView> input);

        Task<Response<ProductCategoryDataModel.DefaultView>> Update(ProductCategoryIdentifier id, ProductCategoryDataModel input);

        Task<Response<ProductCategoryDataModel.DefaultView>> Get(ProductCategoryIdentifier id);

        Task<Response<ProductCategoryDataModel.DefaultView>> Create(ProductCategoryDataModel input);
        ProductCategoryDataModel.DefaultView GetDefault();

        Task<Response> Delete(ProductCategoryIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ProductCategoryAdvancedQuery query);
    }
}

