using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface ICustomerRepository
    {

        Task<ListResponse<CustomerDataModel[]>> Search(
            CustomerAdvancedQuery query);

        Task<ListResponse<CustomerDataModel[]>> BulkUpdate(BatchActionRequest<CustomerIdentifier, CustomerDataModel> data);

        Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input);

        Task<Response<CustomerDataModel>> Get(CustomerIdentifier id);

        Task<Response<CustomerDataModel>> Create(CustomerDataModel input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAdvancedQuery query);
    }
}

