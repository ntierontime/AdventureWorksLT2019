using SQLite;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Framework.MauiX.SQLite;

public static class TableQueryExtensions
{
    public static TableQuery<TSQLiteItem> Sort<TSQLiteItem, U>(this TableQuery<TSQLiteItem> tableQuery, Expression<Func<TSQLiteItem, U>> orderByExpression, Framework.Models.QueryOrderDirections direction)
    {
        if (direction == Framework.Models.QueryOrderDirections.Ascending)
            return tableQuery.OrderBy(orderByExpression);
        return tableQuery.OrderByDescending(orderByExpression);
    }
    public static TableQuery<TSQLiteItem> ThenSort<TSQLiteItem, U>(this TableQuery<TSQLiteItem> tableQuery, Expression<Func<TSQLiteItem, U>> orderByExpression, Framework.Models.QueryOrderDirections direction)
    {
        if (direction == Framework.Models.QueryOrderDirections.Ascending)
            return tableQuery.ThenBy(orderByExpression);
        return tableQuery.ThenByDescending(orderByExpression);
    }
}

public abstract class SQLiteTableRepositoryBase<TItem, TAdvancedQuery, TIIdentifier>
    : SQLiteTableRepositoryBase<TItem>
    where TItem : new()
    where TAdvancedQuery : Framework.MauiX.DataModels.ObservableBaseQuery, new()
{
    public SQLiteTableRepositoryBase(Framework.MauiX.SQLite.SQLiteService sqLiteService)
        : base(sqLiteService)
    {
    }

    protected virtual Expression<Func<TItem, bool>> GetItemExpression(TItem item)
    {
        throw new NotImplementedException("Please implement GetItemExpression in SqliteRepository");
    }

    protected virtual Expression<Func<TItem, bool>> GetItemExpression(TIIdentifier identifier)
    {
        throw new NotImplementedException("Please implement GetItemExpression in SqliteRepository");
    }

    public virtual async Task<TItem> Get(TIIdentifier identifier)
    {
        return await GetItemFromTableAsync(GetItemExpression(identifier));
    }

    public virtual async Task Save(IEnumerable<TItem> list)
    {
        foreach (var item in list)
        {
            await Save(item);
        }
    }

    public virtual async Task Save(TItem item)
    {
        await InsertUpdateItemInTableAsync(GetItemExpression(item), item);
    }

    public virtual async Task Delete(List<TIIdentifier> ids)
    {
        if (ids == null)
            return;
        foreach (var id in ids)
            await DeleteItemFromTableAsync(GetItemExpression(id));
    }

    public virtual async Task Delete(TIIdentifier identifier)
    {
        await DeleteItemFromTableAsync(GetItemExpression(identifier));
    }

    public async Task<int> TotalCount(TAdvancedQuery query)
    {
        var predicate = GetSQLiteTableQueryPredicateByAdvancedQuery(query);
        return await Task.FromResult(_database.Table<TItem>().Count(predicate));
    }

    public virtual async Task<List<TItem>> Search(
        TAdvancedQuery query, Framework.MauiX.DataModels.ObservableQueryOrderBySetting queryOrderBySetting)
    {
        return await Search(query, queryOrderBySetting.Direction, (Func<TableQuery<TItem>, Framework.Models.QueryOrderDirections, TableQuery<TItem>>)queryOrderBySetting.SortFunc);
    }

    protected async Task<List<TItem>> Search(
        TAdvancedQuery query
        , Framework.Models.QueryOrderDirections direction
        , Func<TableQuery<TItem>, Framework.Models.QueryOrderDirections, TableQuery<TItem>> sortFunction)
    {
        var predicate = GetSQLiteTableQueryPredicateByAdvancedQuery(query);

        var tableQuery = _database.Table<TItem>().Where(predicate);

        if (sortFunction != null)
            tableQuery = sortFunction(tableQuery, direction);

        tableQuery = tableQuery.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);

        return
            await Task.FromResult((from t in tableQuery
                                   select (TItem)t).ToList());
    }

    /// <summary>
    /// this expression will be used in Search(TAdvancedQuery query) to filter data in SQLite table
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    protected abstract Expression<Func<TItem, bool>> GetSQLiteTableQueryPredicateByAdvancedQuery(TAdvancedQuery criteria);

}

public class SQLiteTableRepositoryBase<TItem>
    where TItem : new()
{
    private readonly Framework.MauiX.SQLite.SQLiteService _sqLiteService;
    protected SQLiteConnection _database;

    public SQLiteTableRepositoryBase(Framework.MauiX.SQLite.SQLiteService sqLiteService)
    {
        try
        {
            _sqLiteService = sqLiteService;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _database = _sqLiteService.GetDatabase(Path.Combine(path, "LocalSQLite.db3"));
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Debug.WriteLine(@"Sync error: {0}", e.Message);
            throw;
        }
    }

    public async Task<List<TItem>> GetAllItemsFromTableAsync()
    {
        try
        {
            return await Task.FromResult(_database.Table<TItem>().ToList());
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected async Task<TItem> GetItemFromTableAsync(Expression<Func<TItem, bool>> predicate)
    {
        try
        {
            return await Task.FromResult<TItem>(_database.Table<TItem>().Where(predicate).FirstOrDefault(default(TItem)));
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected async Task<List<TItem>> GetItemsFromTableAsync(Expression<Func<TItem, bool>> predicate)
    {
        try
        {
            return await Task.FromResult(_database.Table<TItem>().Where(predicate).ToList());
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected async Task<int> GetTotalByPredicate(Expression<Func<TItem, bool>> predicate)
    {
        try
        {
            return await Task.FromResult(_database.Table<TItem>().Where(predicate).ToList().Count);
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected Task<List<TItem>> GetItemRangeFromTableAsync<U>(Expression<Func<TItem, bool>> predicate, int limit, Expression<Func<TItem, U>> orderExp, string orderType)
    {
        try
        {
            List<TItem> retItem = new();
            if (string.IsNullOrEmpty(orderType) || orderType.ToLower() == "asc")
            {
                retItem = _database.Table<TItem>().Where(predicate).OrderBy(orderExp).Take(limit).ToList();
            }
            else
            {
                retItem = _database.Table<TItem>().Where(predicate).OrderByDescending(orderExp).Take(limit).ToList();
            }
            return Task.FromResult(retItem);
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


    }

    protected Task<List<TItem>> GetItemRangeFromTableAsync<U>(Expression<Func<TItem, bool>> predicate, int skip, int take, Expression<Func<TItem, U>> orderExp, Framework.Models.QueryOrderDirections orderDirections = Framework.Models.QueryOrderDirections.Ascending)
    {
        try
        {
            TableQuery<TItem> tableQuery = _database.Table<TItem>().Where(predicate);
            List<TItem> retItem = new();

            if (orderDirections == Framework.Models.QueryOrderDirections.Ascending)
            {
                tableQuery = tableQuery.OrderBy(orderExp).Skip(skip).Take(take);
            }
            else
            {
                tableQuery = tableQuery.OrderByDescending(orderExp).Skip(skip).Take(take);
            }
            retItem = tableQuery.ToList();

            return Task.FromResult(retItem);
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    protected Task<int> InsertItemIntoTableAsync(TItem item)
    {
        try
        {
            return Task.FromResult(_database.Insert(item));
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

#pragma warning disable CS0162 // Unreachable code detected
        return Task.FromResult(-1);
#pragma warning restore CS0162 // Unreachable code detected
    }

    protected Task<int> DeleteItemFromTableAsync(Expression<Func<TItem, bool>> predicate)
    {
        var retItem = -1;
        try
        {
            TItem item = GetItemFromTableAsync(predicate).Result;
            if (item != null) retItem = _database.Delete(item);
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Task.FromResult(retItem);
    }

    protected Task<int> DeleteAllItemsFromTableAsync()
    {
        try
        {
            return Task.FromResult(_database.DeleteAll<TItem>());
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

#pragma warning disable CS0162 // Unreachable code detected
        return Task.FromResult(-1);
#pragma warning restore CS0162 // Unreachable code detected
    }

    protected Task<int> UpdateItemInTableAsync(TItem item)
    {
        try
        {
            return Task.FromResult(_database.InsertOrReplace(item));
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

#pragma warning disable CS0162 // Unreachable code detected
        return Task.FromResult(-1);
#pragma warning restore CS0162 // Unreachable code detected
    }

    protected Task<int> InsertUpdateItemInTableAsync(Expression<Func<TItem, bool>> predicate, TItem item)
    {
        var retItem = GetItemFromTableAsync(predicate).Result;

        try
        {
            if (retItem == null)
            {
                return Task.FromResult(_database.Insert(item));
            }
            else
            {
                return Task.FromResult(_database.InsertOrReplace(item));
            }
        }
        //catch (MobileServiceInvalidOperationException msioe)
        //{
        //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    throw;
        //}
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected Task<List<TItem>> Execute(string sql, params object[] paramVals)
    {
        try
        {
            List<TItem> ret = new();
            SQLiteCommand cmd = _database.CreateCommand(sql, paramVals);
            ret = cmd.ExecuteQuery<TItem>();

            return Task.FromResult(ret);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    protected Task<int> ExecuteNonQuery(string sql, params object[] paramVals)
    {
        try
        {
            int ret = 0;

            SQLiteCommand cmd = _database.CreateCommand(sql, paramVals);
            ret = cmd.ExecuteNonQuery();

            return Task.FromResult(ret);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}

