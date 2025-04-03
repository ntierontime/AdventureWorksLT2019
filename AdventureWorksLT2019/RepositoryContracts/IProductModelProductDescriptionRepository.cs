using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IProductModelProductDescriptionRepository
    {

        Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> Search(
            ProductModelProductDescriptionAdvancedQuery query);

        Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> data);

        Task<Response<MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<ProductModelProductDescriptionIdentifier, ProductModelProductDescriptionDataModel.DefaultView> input);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Update(ProductModelProductDescriptionIdentifier id, ProductModelProductDescriptionDataModel.DefaultView input, string[]? toUpdatePropertyList = null);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Get(ProductModelProductDescriptionIdentifier id);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Create(ProductModelProductDescriptionDataModel.DefaultView input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelProductDescriptionAdvancedQuery query);
    }
}

