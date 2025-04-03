using AdventureWorksLT2019.Models;
using Framework.Models;
namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IBuildVersionService
    {

        Task<ListResponse<BuildVersionDataModel[]>> Search(
            BuildVersionAdvancedQuery query, ClaimsModel? claimsModel);

        Task<ListResponse<BuildVersionDataModel[]>> BulkUpdate(BatchActionRequest<BuildVersionIdentifier, BuildVersionDataModel> data, ClaimsModel? claimsModel);

        Task<Response<MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel>>> MultiItemsCUD(
            MultiItemsCUDRequest<BuildVersionIdentifier, BuildVersionDataModel> input, ClaimsModel? claimsModel);

        Task<Response<BuildVersionDataModel>> Update(BuildVersionIdentifier id, BuildVersionDataModel input, ClaimsModel? claimsModel, string[]? toUpdatePropertyList = null);

        Task<Response<BuildVersionDataModel>> Get(BuildVersionIdentifier id, ClaimsModel? claimsModel);

        Task<Response<BuildVersionDataModel>> Create(BuildVersionDataModel input, ClaimsModel? claimsModel);
        BuildVersionDataModel GetDefault();

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            BuildVersionAdvancedQuery query, ClaimsModel? claimsModel);
    }
}

