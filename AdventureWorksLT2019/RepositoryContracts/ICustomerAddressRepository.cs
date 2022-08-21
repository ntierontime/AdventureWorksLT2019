using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface ICustomerAddressRepository
    {

        Task<PagedResponse<CustomerAddressDataModel.DefaultView[]>> Search(
            CustomerAddressAdvancedQuery query);

        Task<Response> BulkDelete(List<CustomerAddressIdentifier> ids);

        Task<Response<MultiItemsCUDModel<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> input);

        Task<Response<CustomerAddressDataModel.DefaultView>> Update(CustomerAddressIdentifier id, CustomerAddressDataModel input);

        Task<Response<CustomerAddressDataModel.DefaultView>> Get(CustomerAddressIdentifier id);

        Task<Response<CustomerAddressDataModel.DefaultView>> Create(CustomerAddressDataModel input);

        Task<Response> Delete(CustomerAddressIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            CustomerAddressAdvancedQuery query);
    }
}

