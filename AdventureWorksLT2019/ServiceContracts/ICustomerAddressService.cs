using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ICustomerAddressService
    {

        Task<ListResponse<CustomerAddressDataModel.DefaultView[]>> Search(
            CustomerAddressAdvancedQuery query);

        Task<CustomerAddressCompositeModel> GetCompositeModel(
            CustomerAddressIdentifier id,
            Dictionary<CustomerAddressCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            CustomerAddressCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<CustomerAddressIdentifier> ids);

        Task<Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> input);

        Task<Response<CustomerAddressDataModel.DefaultView>> Update(CustomerAddressIdentifier id, CustomerAddressDataModel input);

        Task<Response<CustomerAddressDataModel.DefaultView>> Get(CustomerAddressIdentifier id);

        Task<Response<CustomerAddressDataModel.DefaultView>> Create(CustomerAddressDataModel input);
        CustomerAddressDataModel.DefaultView GetDefault();

        Task<Response> Delete(CustomerAddressIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAddressAdvancedQuery query);

        Task<Response<CustomerAddressDataModel.DefaultView>> CreateComposite(CustomerAddressCompositeModel input);
    }
}

