using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductDescriptionService
    {

        Task<ListResponse<ProductDescriptionDataModel[]>> Search(
            ProductDescriptionAdvancedQuery query);

        Task<Response<ProductDescriptionDataModel>> Update(ProductDescriptionIdentifier id, ProductDescriptionDataModel input);

        Task<Response<ProductDescriptionDataModel>> Get(ProductDescriptionIdentifier id);

        Task<Response<ProductDescriptionDataModel>> Create(ProductDescriptionDataModel input);
        ProductDescriptionDataModel GetDefault();

        Task<Response> Delete(ProductDescriptionIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductDescriptionAdvancedQuery query);
    }
}

