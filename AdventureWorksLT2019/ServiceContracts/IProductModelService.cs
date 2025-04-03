using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductModelService
    {

        Task<ListResponse<ProductModelDataModel[]>> Search(
            ProductModelAdvancedQuery query, ClaimsModel? claimsModel);

        Task<Response<ProductModelDataModel>> Update(ProductModelIdentifier id, ProductModelDataModel input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<ProductModelDataModel>> Get(ProductModelIdentifier id, ClaimsModel? claimsModel);

        Task<Response<ProductModelDataModel>> Create(ProductModelDataModel input, ClaimsModel? claimsModel);
        ProductModelDataModel GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

