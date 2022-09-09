using SQLite;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Framework.MauiX.SQLite
{
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

    public abstract class SQLiteTableRepositoryBase<TItem, TAdvancedCriteria, TIIdentifier>
        : SQLiteTableRepositoryBase<TItem>
        where TItem : new()
        where TAdvancedCriteria : Framework.MauiX.DataModels.ObservableBaseQuery, new()
    {
        public SQLiteTableRepositoryBase(Framework.MauiX.SQLite.SQLiteService sqLiteService)
            : base(sqLiteService)
        {
        }

        protected virtual Expression<Func<TItem, bool>> GetItemExpression(TIIdentifier identifier)
        {
            throw new NotImplementedException("Please implement GetItemExpression in SqliteRepository");
        }

        public virtual async Task<TItem> Get(TIIdentifier identifier)
        {
            return await Task.FromResult(new TItem());
        }

        //public virtual async Task Save(List<TItem> list)
        //{
        //    foreach (var item in list)
        //    {
        //        await Save(item);
        //    }
        //}

        public virtual async Task Save(TIIdentifier identifier, TItem item)
        {
            await Task.FromException(new NotImplementedException());
        }

        public virtual async Task Delete(TIIdentifier identifier)
        {
            await DeleteItemFromTableAsync(GetItemExpression(identifier));
        }

        public async Task<int> Count(TAdvancedCriteria criteria)
        {
            var predicate = GetSQLiteTableQueryPredicate_Common(criteria);
            return await Task.FromResult(_database.Table<TItem>().Count(predicate));
        }

        public async Task<List<TItem>> Load(
            TAdvancedCriteria criteria
            , Framework.Models.QueryOrderBySetting queryOrderBySetting
            , Func<TableQuery<TItem>, Framework.Models.QueryOrderDirections, TableQuery<TItem>> sortFunction)
        {
            var predicate = GetSQLiteTableQueryPredicate_Common(criteria);

            var tableQuery = _database.Table<TItem>().Where(predicate);

            if (sortFunction != null)
                tableQuery = sortFunction(tableQuery, queryOrderBySetting.Direction);

            tableQuery = tableQuery.Skip((criteria.PageIndex - 1) * criteria.PageSize).Take(criteria.PageSize);

            return
                await Task.FromResult((from t in tableQuery
                                       select (TItem)t).ToList());
        }

        protected abstract Expression<Func<TItem, bool>> GetSQLiteTableQueryPredicate_Common(TAdvancedCriteria criteria);

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

        public Task<List<TItem>> GetAllItemsFromTableAsync()
        {
            List<TItem> retItem = new List<TItem>();
            try
            {
                retItem = _database.Table<TItem>().ToList();
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

        public Task<TItem> GetItemFromTableAsync(Expression<Func<TItem, bool>> predicate)
        {
            TItem retItem = new TItem();
            try
            {
                retItem = _database.Table<TItem>().Where(predicate).FirstOrDefault();
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

        public Task<List<TItem>> GetItemsFromTableAsync(Expression<Func<TItem, bool>> predicate)
        {
            List<TItem> retItem = new List<TItem>();

            try
            {
                retItem = _database.Table<TItem>().Where(predicate).ToList();
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

        public Task<int> GetTotalByPredicate(Expression<Func<TItem, bool>> predicate)
        {
            int retItem = 0;

            try
            {
                retItem = _database.Table<TItem>().Where(predicate).ToList().Count;
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

        public Task<List<TItem>> GetItemRangeFromTableAsync<U>(Expression<Func<TItem, bool>> predicate, int limit, Expression<Func<TItem, U>> orderExp, string orderType)
        {
            List<TItem> retItem = new List<TItem>();

            try
            {
                if (string.IsNullOrEmpty(orderType) || orderType.ToLower() == "asc")
                {
                    retItem = _database.Table<TItem>().Where(predicate).OrderBy(orderExp).Take(limit).ToList();
                }
                else
                {
                    retItem = _database.Table<TItem>().Where(predicate).OrderByDescending(orderExp).Take(limit).ToList();
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

            return Task.FromResult(retItem);
        }

        public Task<List<TItem>> GetItemRangeFromTableAsync<U>(Expression<Func<TItem, bool>> predicate, int skip, int take, Expression<Func<TItem, U>> orderExp, Framework.Models.QueryOrderDirections orderDirections = Framework.Models.QueryOrderDirections.Ascending)
        {
            TableQuery<TItem> tableQuery = _database.Table<TItem>().Where(predicate);
            List<TItem> retItem = new List<TItem>();

            try
            {
                if (orderDirections == Framework.Models.QueryOrderDirections.Ascending)
                {
                    tableQuery = tableQuery.OrderBy(orderExp).Skip(skip).Take(take);
                }
                else
                {
                    tableQuery = tableQuery.OrderByDescending(orderExp).Skip(skip).Take(take);
                }
                retItem = tableQuery.ToList();
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

        public Task<int> InsertItemIntoTableAsync(TItem item)
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

        public Task<int> DeleteItemFromTableAsync(Expression<Func<TItem, bool>> predicate)
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

        public Task<int> DeleteAllItemsFromTableAsync()
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

        public Task<int> UpdateItemInTableAsync(TItem item)
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

        public Task<int> InsertUpdateItemInTableAsync(Expression<Func<TItem, bool>> predicate, TItem item)
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

        public Task<List<TItem>> Execute(string sql, params object[] paramVals)
        {
            List<TItem> ret = new List<TItem>();

            try
            {
                SQLiteCommand cmd = _database.CreateCommand(sql, paramVals);
                ret = cmd.ExecuteQuery<TItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return Task.FromResult(ret);
        }

        public Task<int> ExecuteNonQuery(string sql, params object[] paramVals)
        {
            int ret = 0;

            try
            {
                SQLiteCommand cmd = _database.CreateCommand(sql, paramVals);
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return Task.FromResult(ret);
        }
    }
}

