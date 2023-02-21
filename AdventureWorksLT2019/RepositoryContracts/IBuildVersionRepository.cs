using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IBuildVersionRepository
    {

        Task<ListResponse<BuildVersionDataModel[]>> Search(
            BuildVersionAdvancedQuery query);

        Task<Response<BuildVersionDataModel>> Update(BuildVersionIdentifier id, BuildVersionDataModel input);

        Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id);

        Task<Response<BuildVersionDataModel>> Create(BuildVersionDataModel input);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            BuildVersionAdvancedQuery query);
    }
}

