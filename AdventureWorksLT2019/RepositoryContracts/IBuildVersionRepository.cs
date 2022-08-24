using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.RepositoryContracts
{
    public interface IBuildVersionRepository
    {

        Task<PagedResponse<BuildVersionDataModel[]>> Search(
            BuildVersionAdvancedQuery query);

        Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id);
    }
}

