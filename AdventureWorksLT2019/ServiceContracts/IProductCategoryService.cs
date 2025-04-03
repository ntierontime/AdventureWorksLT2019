using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductCategoryService
    {

        Task<ListResponse<ProductCategoryDataModel.DefaultView[]>> Search(
            ProductCategoryAdvancedQuery query, ClaimsModel? claimsModel);

        Task<Response<ProductCategoryDataModel.DefaultView>> Update(ProductCategoryIdentifier id, ProductCategoryDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<ProductCategoryDataModel.DefaultView>> Get(ProductCategoryIdentifier id, ClaimsModel? claimsModel);

        Task<Response<ProductCategoryDataModel.DefaultView>> Create(ProductCategoryDataModel.DefaultView input, ClaimsModel? claimsModel);
        ProductCategoryDataModel.DefaultView GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductCategoryAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

