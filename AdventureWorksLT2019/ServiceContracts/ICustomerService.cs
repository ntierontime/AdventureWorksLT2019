using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ICustomerService
    {

        Task<ListResponse<CustomerDataModel[]>> Search(
            CustomerAdvancedQuery query);

        Task<CustomerCompositeModel> GetCompositeModel(
            CustomerIdentifier id,
            Dictionary<CustomerCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            CustomerCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<ListResponse<CustomerDataModel[]>> BulkUpdate(BatchActionRequest<CustomerIdentifier, CustomerDataModel> data);

        Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input);

        Task<Response<CustomerDataModel>> Get(CustomerIdentifier id);

        Task<Response<CustomerDataModel>> Create(CustomerDataModel input);
        CustomerDataModel GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAdvancedQuery query);
    }
}

