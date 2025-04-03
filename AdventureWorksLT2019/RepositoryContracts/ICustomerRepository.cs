using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface ICustomerRepository
    {

        Task<ListResponse<CustomerDataModel[]>> Search(
            CustomerAdvancedQuery query);

        Task<Response<CustomerDataModel>> Update(CustomerIdentifier id, CustomerDataModel input, string[]? toUpdatePropertyList = null);

        Task<Response<CustomerDataModel>> Get(CustomerIdentifier id);

        Task<Response<CustomerDataModel>> Create(CustomerDataModel input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            CustomerAdvancedQuery query);
    }
}

