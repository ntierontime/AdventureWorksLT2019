using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IBuildVersionService
    {

        Task<ListResponse<BuildVersionDataModel[]>> Search(
            BuildVersionAdvancedQuery query);

        Task<Response<BuildVersionDataModel>> Update(BuildVersionIdentifier id, BuildVersionDataModel input);

        Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id);

        Task<Response<BuildVersionDataModel>> Create(BuildVersionDataModel input);
        BuildVersionDataModel GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            BuildVersionAdvancedQuery query);
    }
}

