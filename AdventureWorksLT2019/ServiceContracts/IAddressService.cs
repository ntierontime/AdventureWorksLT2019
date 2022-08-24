using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IAddressService
    {

        Task<PagedResponse<AddressDataModel[]>> Search(
            AddressAdvancedQuery query);

        Task<Response<AddressDataModel>> Update(AddressIdentifier id, AddressDataModel input);

        Task<Response<AddressDataModel>> Get(AddressIdentifier id);

        Task<Response<AddressDataModel>> Create(AddressDataModel input);
        AddressDataModel GetDefault();
    }
}

