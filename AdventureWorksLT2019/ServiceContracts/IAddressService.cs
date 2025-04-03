using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IAddressService
    {

        Task<ListResponse<AddressDataModel[]>> Search(
            AddressAdvancedQuery query, ClaimsModel? claimsModel);

        Task<Response<AddressDataModel>> Update(AddressIdentifier id, AddressDataModel input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<AddressDataModel>> Get(AddressIdentifier id, ClaimsModel? claimsModel);

        Task<Response<AddressDataModel>> Create(AddressDataModel input, ClaimsModel? claimsModel);
        AddressDataModel GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            AddressAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

