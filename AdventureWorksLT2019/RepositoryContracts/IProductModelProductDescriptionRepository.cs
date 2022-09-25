using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IProductModelProductDescriptionRepository
    {

        Task<ListResponse<ProductModelProductDescriptionDataModel.DefaultView[]>> Search(
            ProductModelProductDescriptionAdvancedQuery query);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Update(ProductModelProductDescriptionIdentifier id, ProductModelProductDescriptionDataModel input);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Get(ProductModelProductDescriptionIdentifier id);

        Task<Response<ProductModelProductDescriptionDataModel.DefaultView>> Create(ProductModelProductDescriptionDataModel input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ProductModelProductDescriptionAdvancedQuery query);
    }
}

