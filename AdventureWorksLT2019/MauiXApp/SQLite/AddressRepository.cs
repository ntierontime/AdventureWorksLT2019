using AdventureWorksLT2019.MauiXApp.DataModels;
using Framework.MauiX.DataModels;
using Framework.MauiX.SQLite;
using Framework.Models;
using SQLite;
using System.Linq.Expressions;

namespace AdventureWorksLT2019.MauiXApp.SQLite;

public class AddressRepository : SQLiteTableRepositoryBase<AddressDataModel, AddressAdvancedQuery, AddressIdentifier>
{
    public AddressRepository(SQLiteService sqLiteService) : base(sqLiteService)
    {
        _database.CreateTable<AddressDataModel>();
    }

    protected override Expression<Func<AddressDataModel, bool>> GetItemExpression(AddressDataModel item)
    {
        return t => t.AddressID == item.AddressID;
    }

    protected override Expression<Func<AddressDataModel, bool>> GetItemExpression(AddressIdentifier identifier)
    {
        return t => t.AddressID == identifier.AddressID;
    }

    /// <summary>
    /// this expression will be used in Search(TAdvancedQuery query) to filter data in SQLite table
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    protected override Expression<Func<AddressDataModel, bool>> GetSQLiteTableQueryPredicateByAdvancedQuery(AddressAdvancedQuery query)
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

