using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ICustomerService
    {

        Task<ListResponse<CustomerDataModel[]>> Search(
            CustomerAdvancedQuery query, ClaimsModel? claimsModel);

        Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<CustomerDataModel>> Get(CustomerIdentifier id, ClaimsModel? claimsModel);

        Task<Response<CustomerDataModel>> Create(CustomerDataModel input, ClaimsModel? claimsModel);
        CustomerDataModel GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

