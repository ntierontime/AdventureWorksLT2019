using SQLite;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Framework.MauiX.SQLite;

/// <summary>
/// All cached data tables must have a LastModifiedDateTime, and/or CreatedDateTime
/// </summary>
public class CacheDataStatusService
{
    private readonly Framework.MauiX.SQLite.CacheDataStatusRepository _cacheDataStatusRepository;
    public CacheDataStatusService(
        Framework.MauiX.SQLite.CacheDataStatusRepository cacheDataStatusRepository)
    {
        _cacheDataStatusRepository = cacheDataStatusRepository;
    }

    public async Task<Framework.MauiX.DataModels.CacheDataStatusItem> Get(string key)
    {
        var cachedItem = await _cacheDataStatusRepository.Get(key);
        if(cachedItem == null)
        {
            cachedItem = new Framework.MauiX.DataModels.CacheDataStatusItem
            {
                Key = key,
            };
            await _cacheDataStatusRepository.Save(cachedItem);
        }

        return cachedItem;
    }

    public async Task SyncedServerData(string key)
    {
        var cachedItem = await Get(key);
        cachedItem.LastSyncDateTime = DateTime.Now;
        cachedItem.UpdatedInServer = false;
        await _cacheDataStatusRepository.Save(cachedItem);
    }

    public async Task ClientSideDataNotSynced(string key)
    {
        var cachedItem = await Get(key);
        cachedItem.UpdatedInClient = true;
        await _cacheDataStatusRepository.Save(cachedItem);
    }
}
