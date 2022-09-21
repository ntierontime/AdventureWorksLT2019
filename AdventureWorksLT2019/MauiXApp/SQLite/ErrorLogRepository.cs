using AdventureWorksLT2019.MauiXApp.DataModels;
using Framework.MauiX.DataModels;
using Framework.MauiX.SQLite;
using Framework.Models;
using SQLite;
using System.Linq.Expressions;

namespace AdventureWorksLT2019.MauiXApp.SQLite;

public class ErrorLogRepository : SQLiteTableRepositoryBase<ErrorLogDataModel, ErrorLogAdvancedQuery, ErrorLogIdentifier>
{
    public ErrorLogRepository(SQLiteService sqLiteService) : base(sqLiteService)
    {
        _database.CreateTable<ErrorLogDataModel>();
    }

    protected override Expression<Func<ErrorLogDataModel, bool>> GetItemExpression(ErrorLogDataModel item)
    {
        return t => t.ErrorLogID == item.ErrorLogID;
    }

    protected override Expression<Func<ErrorLogDataModel, bool>> GetItemExpression(ErrorLogIdentifier identifier)
    {
        return t => t.ErrorLogID == identifier.ErrorLogID;
    }

    /// <summary>
    /// this expression will be used in Search(TAdvancedQuery query) to filter data in SQLite table
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    protected override Expression<Func<ErrorLogDataModel, bool>> GetSQLiteTableQueryPredicateByAdvancedQuery(ErrorLogAdvancedQuery query)
    {
        return t => true;
        // TODO: To make query simple: text search will be applied to all text fields.
        /*
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
        */
    }
}

