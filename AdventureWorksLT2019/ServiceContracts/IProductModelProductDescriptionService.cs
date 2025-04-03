using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IProductModelProductDescriptionService
    {

        Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> Search(
            ProductModelProductDescriptionAdvancedQuery query, ClaimsModel? claimsModel);

        Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> data, ClaimsModel? claimsModel);

        Task<Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> input, ClaimsModel? claimsModel);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Update(ProductModelProductDescriptionIdentifier id, ProductModelProductDescriptionDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Get(ProductModelProductDescriptionIdentifier id, ClaimsModel? claimsModel);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Create(ProductModelProductDescriptionDataModel.DefaultView input, ClaimsModel? claimsModel);
        ProductModelProductDescriptionDataModel.DefaultView GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelProductDescriptionAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

