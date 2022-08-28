using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface ICustomerService
    {

        Task<PagedResponse<CustomerDataModel[]>> Search(
            CustomerAdvancedQuery query);

        Task<CustomerCompositeModel> GetCompositeModel(
            CustomerIdentifier id,
            Dictionary<CustomerCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            CustomerCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<CustomerIdentifier> ids);

        Task<PagedResponse<CustomerDataModel[]>> BulkUpdate(BatchActionViewModel<CustomerIdentifier, CustomerDataModel> data);

        Task<Response<MultiItemsCUDModel<CustomerIdentifier, CustomerDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<CustomerIdentifier, CustomerDataModel> input);

        Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input);

        Task<Response<CustomerDataModel>> Get(CustomerIdentifier id);

        Task<Response<CustomerDataModel>> Create(CustomerDataModel input);
        CustomerDataModel GetDefault();

        Task<Response> Delete(CustomerIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            CustomerAdvancedQuery query);
    }
}

