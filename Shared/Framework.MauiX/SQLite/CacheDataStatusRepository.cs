using Framework.MauiX.DataModels;
namespace Framework.MauiX.SQLite;

public class CacheDataStatusRepository : SQLiteTableRepositoryBase<CacheDataStatusItem>
{
    public CacheDataStatusRepository(SQLiteService sqLiteService) : base(sqLiteService)
    {
        _database.CreateTable<CacheDataStatusItem>();
    }

    public async Task Save(CacheDataStatusItem item)
    {
        await UpdateItemInTableAsync(item);
    }
    public async Task<CacheDataStatusItem> Get(string key)
    {
        return await GetItemFromTableAsync(item => item.Key == key);
    }
}

