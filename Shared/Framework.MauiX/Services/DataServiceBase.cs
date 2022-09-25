using Framework.MauiX.DataModels;
using Framework.Models;

namespace Framework.MauiX.Services;

public abstract class DataServiceBase<TAdvancedQuery, TIdentifier, TDataModel>: IDataServiceBase<TAdvancedQuery, TIdentifier, TDataModel>
    where TDataModel : class, new()
{
    public virtual async Task<Response<TDataModel>> Create(TDataModel input)
    {
        return await Task.FromException<Response<TDataModel>>(new NotImplementedException());
    }
    public virtual async Task<Response<TDataModel>> Update(TIdentifier id, TDataModel input)
    {
        return await Task.FromException<Response<TDataModel>>(new NotImplementedException());
    }
    public virtual async Task<Response> Delete(TIdentifier id)
    {
        return await Task.FromException<Response>(new NotImplementedException());
    }
    public virtual async Task<Response<TDataModel>> Get(TIdentifier id)
    {
        return await Task.FromException<Response<TDataModel>>(new NotImplementedException());
    }
    public virtual TDataModel GetDefault()
    {
        return new();
    }

    public virtual async Task<Response> BulkDelete(List<TIdentifier> ids)
    {
        return await Task.FromException<Response>(new NotImplementedException());
    }
    public virtual async Task<ListResponse<TDataModel[]>> BulkUpdate(BatchActionRequest<TIdentifier, TDataModel> data)
    {
        return await Task.FromException<ListResponse<TDataModel[]>>(new NotImplementedException());
    }
    public virtual async Task<Response<MultiItemsCUDRequest<TIdentifier, TDataModel>>> MultiItemsCUD(MultiItemsCUDRequest<TIdentifier, TDataModel> input)
    {
        return await Task.FromException<Response<MultiItemsCUDRequest<TIdentifier, TDataModel>>>(new NotImplementedException());
    }
    public virtual async Task<ListResponse<TDataModel[]>> Search(TAdvancedQuery query, ObservableQueryOrderBySetting queryOrderBySetting)
    {
        return await Task.FromException<ListResponse<TDataModel[]>>(new NotImplementedException());
    }

    public virtual List<ObservableQueryOrderBySetting> GetQueryOrderBySettings()
    {
        throw new NotImplementedException();
    }
}

