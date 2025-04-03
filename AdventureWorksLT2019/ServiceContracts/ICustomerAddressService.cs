using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ICustomerAddressService
    {

        Task<ListResponse<CustomerAddressDataModel.DefaultView[]>> Search(
            CustomerAddressAdvancedQuery query, ClaimsModel? claimsModel);

        Task<ListResponse<CustomerAddressDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> data, ClaimsModel? claimsModel);

        Task<Response<MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDRequest<CustomerAddressIdentifier, CustomerAddressDataModel.DefaultView> input, ClaimsModel? claimsModel);

        Task<Response<CustomerAddressDataModel.DefaultView>> Update(CustomerAddressIdentifier id, CustomerAddressDataModel.DefaultView input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<CustomerAddressDataModel.DefaultView>> Get(CustomerAddressIdentifier id, ClaimsModel? claimsModel);

        Task<Response<CustomerAddressDataModel.DefaultView>> Create(CustomerAddressDataModel.DefaultView input, ClaimsModel? claimsModel);
        CustomerAddressDataModel.DefaultView GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAddressAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

