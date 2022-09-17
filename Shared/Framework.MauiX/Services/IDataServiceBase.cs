
namespace Framework.MauiX.Services;

public interface IDataServiceBase<TIdentifier, TDataModel>
    where TDataModel : class
{
    Task<Framework.Models.Response<TDataModel>> Create(TDataModel input);
    Task<Framework.Models.Response<TDataModel>> Update(TIdentifier id, TDataModel input);
    Task<Framework.Models.Response> Delete(TIdentifier id);
    Task<Framework.Models.Response<TDataModel>> Get(TIdentifier id);
    TDataModel GetDefault();
}

public interface IDataServiceBase<TAdvancedQuery, TIdentifier, TDataModel> : IDataServiceBase<TIdentifier, TDataModel>
    where TDataModel : class
{
    Task<Framework.Models.Response> BulkDelete(List<TIdentifier> ids);
    Task<Framework.Models.ListResponse<TDataModel[]>> BulkUpdate(Framework.Models.BatchActionRequest<TIdentifier, TDataModel> data);
    Task<Framework.Models.Response<Framework.Models.MultiItemsCUDRequest<TIdentifier, TDataModel>>> MultiItemsCUD(Framework.Models.MultiItemsCUDRequest<TIdentifier, TDataModel> input);
    Task<Framework.Models.ListResponse<TDataModel[]>> Search(TAdvancedQuery query, Framework.MauiX.DataModels.ObservableQueryOrderBySetting queryOrderBySetting);

    List<Framework.MauiX.DataModels.ObservableQueryOrderBySetting> GetQueryOrderBySettings();
}