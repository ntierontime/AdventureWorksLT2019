using SQLite;
using System.Linq.Expressions;

namespace AdventureWorksLT2019.MauiXApp.SQLite
{
    public class CustomerRepository : Framework.MauiX.SQLite.SQLiteTableRepositoryBase<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery, AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier>
    {
        public CustomerRepository(Framework.MauiX.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>();
        }

        public override async Task<List<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>> Search(
            AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery query, Framework.MauiX.DataModels.ObservableQueryOrderBySetting queryOrderBySetting)
        {
            var tableQuery =
                from t in _database.Table<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>()
                where
                    (string.IsNullOrEmpty(query.TextSearch) ||
                        !string.IsNullOrEmpty(t.Title) && t.Title.Contains(query.TextSearch) || !string.IsNullOrEmpty(t.FirstName) && t.FirstName.Contains(query.TextSearch))
                select t;
            tableQuery = tableQuery.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);
            return
            await Task.FromResult((from t in tableQuery
                           select t).ToList());
            // return await Search(query, queryOrderBySetting.Direction, (Func<TableQuery<TItem>, Framework.Models.QueryOrderDirections, TableQuery<TItem>>)queryOrderBySetting.SortFunc);
        }


        protected override Expression<Func<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, bool>> GetItemExpression(AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel item)
        {
            return t => t.CustomerID == item.CustomerID;
        }

        protected override Expression<Func<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, bool>> GetItemExpression(AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier identifier)
        {
            return t => t.CustomerID == identifier.CustomerID;
        }


        protected override Expression<Func<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, bool>> GetSQLiteTableQueryPredicateByAdvancedQuery(AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery query)
        {
            //return t => true;
            // TODO: To make query simple: text search will be applied to all text fields.
            return
                t =>
                (string.IsNullOrEmpty(query.TextSearch) ||
                        !string.IsNullOrEmpty(t.Title) && t.Title.Contains(query.TextSearch) || !string.IsNullOrEmpty(t.FirstName) && t.FirstName.Contains(query.TextSearch)
                        //query.TextSearchType == TextSearchTypes.Contains && t.Title.Contains(query.TextSearch) ||
                        //query.TextSearchType == TextSearchTypes.StartsWith && t.Title.StartsWith(query.TextSearch) ||
                        //query.TextSearchType == TextSearchTypes.EndsWith && t.Title.EndsWith(query.TextSearch)
                        )
                  // &&
                  //  (query.NameStyle == null || t.NameStyle == query.NameStyle)
                  //&&
                  //(query.ModifiedDateRangeLower == null || query.ModifiedDateRangeUpper == null || query.ModifiedDateRangeLower != null && query.ModifiedDateRangeLower <= t.ModifiedDate || query.ModifiedDateRangeUpper != null && query.ModifiedDateRangeUpper >= t.ModifiedDate)
            ;
        }
    }
}

