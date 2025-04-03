using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface ICustomerAddressRepository
    {

        Task<ListResponse<CustomerAddressDataModel.DefaultView[]>> Search(
            CustomerAddressAdvancedQuery query);

        Task<ListResponse<CustomerAddressDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> data);

        Task<Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> input);

        Task<Response<CustomerAddressDataModel.DefaultView>> Update(CustomerAddressIdentifier id, CustomerAddressDataModel.DefaultView input, string[]? toUpdatePropertyList = null);

        Task<Response<CustomerAddressDataModel.DefaultView>> Get(CustomerAddressIdentifier id);

        Task<Response<CustomerAddressDataModel.DefaultView>> Create(CustomerAddressDataModel.DefaultView input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAddressAdvancedQuery query);
    }
}

