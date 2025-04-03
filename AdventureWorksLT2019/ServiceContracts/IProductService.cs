using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductService
    {

        Task<ListResponse<ProductDataModel.DefaultView[]>> Search(
            ProductAdvancedQuery query, ClaimsModel? claimsModel);

        Task<Response<ProductDataModel.DefaultView>> Update(ProductIdentifier id, ProductDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<ProductDataModel.DefaultView>> Get(ProductIdentifier id, ClaimsModel? claimsModel);

        Task<Response<ProductDataModel.DefaultView>> Create(ProductDataModel.DefaultView input, ClaimsModel? claimsModel);
        ProductDataModel.DefaultView GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

