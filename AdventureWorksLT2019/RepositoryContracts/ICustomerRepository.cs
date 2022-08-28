using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface ICustomerRepository
    {

        Task<PagedResponse<CustomerDataModel[]>> Search(
            CustomerAdvancedQuery query);

        Task<Response> BulkDelete(List<CustomerIdentifier> ids);

        Task<PagedResponse<CustomerDataModel[]>> BulkUpdate(BatchActionViewModel<CustomerIdentifier, CustomerDataModel> data);

        Task<Response<MultiItemsCUDModel<CustomerIdentifier, CustomerDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<CustomerIdentifier, CustomerDataModel> input);

        Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input);

        Task<Response<CustomerDataModel>> Get(CustomerIdentifier id);

        Task<Response<CustomerDataModel>> Create(CustomerDataModel input);

        Task<Response> Delete(CustomerIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            CustomerAdvancedQuery query);
    }
}

