using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IBuildVersionService
    {

        Task<ListResponse<BuildVersionDataModel[]>> Search(
            BuildVersionAdvancedQuery query);

        Task<BuildVersionCompositeModel> GetCompositeModel(
            BuildVersionIdentifier id,
            Dictionary<BuildVersionCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            BuildVersionCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<BuildVersionIdentifier> ids);

        Task<Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel> input);

        Task<Response<BuildVersionDataModel>> Update(BuildVersionIdentifier id, BuildVersionDataModel input);

        Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id);

        Task<Response<BuildVersionDataModel>> Create(BuildVersionDataModel input);
        BuildVersionDataModel GetDefault();

        Task<Response> Delete(BuildVersionIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            BuildVersionAdvancedQuery query);
    }
}

