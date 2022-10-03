using Framework.MauiX.DataModels;
using Framework.Models;

namespace Framework.MauiX.Services;

public interface IDataServiceBase<TIdentifier, TDataModel>
    where TDataModel : class
{
    Task<Response<TDataModel>> Create(TDataModel input);
    Task<Response<TDataModel>> Update(TIdentifier id, TDataModel input);
    Task<Response> Delete(TIdentifier id);
    Task<Response<TDataModel>> Get(TIdentifier id);
    TDataModel GetDefault();
}

public interface IDataServiceBase<TAdvancedQuery, TIdentifier, TDataModel> : IDataServiceBase<TIdentifier, TDataModel>
    where TDataModel : class
{
    Task<Response> BulkDelete(List<TIdentifier> ids);
    Task<ListResponse<TDataModel[]>> BulkUpdate(BatchActionRequest<TIdentifier, TDataModel> data);
    Task<Response<MultiItemsCUDRequest<TIdentifier, TDataModel>>> MultiItemsCUD(MultiItemsCUDRequest<TIdentifier, TDataModel> input);
    Task<ListResponse<TDataModel[]>> Search(TAdvancedQuery query, ObservableQueryOrderBySetting queryOrderBySetting);

    List<ObservableQueryOrderBySetting> GetQueryOrderBySettings();
}

