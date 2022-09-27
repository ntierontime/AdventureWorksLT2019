using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IAddressService
    {

        Task<ListResponse<AddressDataModel[]>> Search(
            AddressAdvancedQuery query);

        Task<AddressCompositeModel> GetCompositeModel(
            AddressIdentifier id,
            Dictionary<AddressCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            AddressCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<AddressIdentifier> ids);

        Task<Response<MultiItemsCUDRequest<AddressIdentifier, AddressDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<AddressIdentifier, AddressDataModel> input);

        Task<Response<AddressDataModel>> Update(AddressIdentifier id, AddressDataModel input);

        Task<Response<AddressDataModel>> Get(AddressIdentifier id);

        Task<Response<AddressDataModel>> Create(AddressDataModel input);
        AddressDataModel GetDefault();

        Task<Response> Delete(AddressIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            AddressAdvancedQuery query);
    }
}

