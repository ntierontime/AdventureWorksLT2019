using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ICustomerAddressService
    {

        Task<ListResponse<CustomerAddressDataModel.DefaultView[]>> Search(
            CustomerAddressAdvancedQuery query);

        Task<Response<CustomerAddressDataModel.DefaultView>> Update(CustomerAddressIdentifier id, CustomerAddressDataModel input);

        Task<Response<CustomerAddressDataModel.DefaultView>> Get(CustomerAddressIdentifier id);

        Task<Response<CustomerAddressDataModel.DefaultView>> Create(CustomerAddressDataModel input);
        CustomerAddressDataModel.DefaultView GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAddressAdvancedQuery query);
    }
}

