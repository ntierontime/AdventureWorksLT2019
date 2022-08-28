using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IAddressRepository
    {

        Task<PagedResponse<AddressDataModel[]>> Search(
            AddressAdvancedQuery query);

        Task<Response> BulkDelete(List<AddressIdentifier> ids);

        Task<Response<MultiItemsCUDModel<AddressIdentifier, AddressDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<AddressIdentifier, AddressDataModel> input);

        Task<Response<AddressDataModel>> Update(AddressIdentifier id, AddressDataModel input);

        Task<Response<AddressDataModel>> Get(AddressIdentifier id);

        Task<Response<AddressDataModel>> Create(AddressDataModel input);

        Task<Response> Delete(AddressIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            AddressAdvancedQuery query);
    }
}

