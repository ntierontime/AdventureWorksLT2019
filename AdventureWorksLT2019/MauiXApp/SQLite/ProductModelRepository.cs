using AdventureWorksLT2019.MauiXApp.DataModels;
using Framework.MauiX.DataModels;
using Framework.MauiX.SQLite;
using Framework.Models;
using SQLite;
using System.Linq.Expressions;

namespace AdventureWorksLT2019.MauiXApp.SQLite;

public class ProductModelRepository : SQLiteTableRepositoryBase<ProductModelDataModel, ProductModelAdvancedQuery, ProductModelIdentifier>
{
    public ProductModelRepository(SQLiteService sqLiteService) : base(sqLiteService)
    {
        _database.CreateTable<ProductModelDataModel>();
    }

    protected override Expression<Func<ProductModelDataModel, bool>> GetItemExpression(ProductModelDataModel item)
    {
        return t => t.ProductModelID == item.ProductModelID;
    }

    protected override Expression<Func<ProductModelDataModel, bool>> GetItemExpression(ProductModelIdentifier identifier)
    {
        return t => t.ProductModelID == identifier.ProductModelID;
    }

    /// <summary>
    /// this expression will be used in Search(TAdvancedQuery query) to filter data in SQLite table
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    protected override Expression<Func<ProductModelDataModel, bool>> GetSQLiteTableQueryPredicateByAdvancedQuery(ProductModelAdvancedQuery query)
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

