using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductDescriptionService
    {

        Task<ListResponse<ProductDescriptionDataModel[]>> Search(
            ProductDescriptionAdvancedQuery query, ClaimsModel? claimsModel);

        Task<Response<ProductDescriptionDataModel>> Update(ProductDescriptionIdentifier id, ProductDescriptionDataModel input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<ProductDescriptionDataModel>> Get(ProductDescriptionIdentifier id, ClaimsModel? claimsModel);

        Task<Response<ProductDescriptionDataModel>> Create(ProductDescriptionDataModel input, ClaimsModel? claimsModel);
        ProductDescriptionDataModel GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductDescriptionAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

