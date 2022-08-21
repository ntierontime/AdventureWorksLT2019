using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IBuildVersionService
    {

        Task<PagedResponse<BuildVersionDataModel[]>> Search(
            BuildVersionAdvancedQuery query);

        Task<BuildVersionCompositeModel> GetCompositeModel(
            BuildVersionIdentifier id, BuildVersionCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<BuildVersionIdentifier> ids);

        Task<Response<MultiItemsCUDModel<BuildVersionIdentifier, BuildVersionDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<BuildVersionIdentifier, BuildVersionDataModel> input);

        Task<Response<BuildVersionDataModel>> Update(BuildVersionIdentifier id, BuildVersionDataModel input);

        Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id);

        Task<Response<BuildVersionDataModel>> Create(BuildVersionDataModel input);
        BuildVersionDataModel GetDefault();

        Task<Response> Delete(BuildVersionIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            BuildVersionAdvancedQuery query);
    }
}

