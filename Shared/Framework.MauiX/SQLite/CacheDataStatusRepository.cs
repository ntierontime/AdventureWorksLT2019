using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MauiX.SQLite;

public class CacheDataStatusRepository : Framework.MauiX.SQLite.SQLiteTableRepositoryBase<Framework.MauiX.DataModels.CacheDataStatusItem>
{
    public CacheDataStatusRepository(Framework.MauiX.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
    {
        _database.CreateTable<Framework.MauiX.DataModels.CacheDataStatusItem>();
    }

    public async Task Save(Framework.MauiX.DataModels.CacheDataStatusItem item)
    {
        await UpdateItemInTableAsync(item);
    }
    public async Task<Framework.MauiX.DataModels.CacheDataStatusItem> Get(string key)
    {
        return await GetItemFromTableAsync(item => item.Key == key);
    }
}
