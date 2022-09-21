using AdventureWorksLT2019.MauiXApp.DataModels;
using Framework.MauiX.DataModels;
using Framework.MauiX.SQLite;
using Framework.Models;
using SQLite;
using System.Linq.Expressions;

namespace AdventureWorksLT2019.MauiXApp.SQLite;

public class ProductModelProductDescriptionRepository : SQLiteTableRepositoryBase<ProductModelProductDescriptionDataModel, ProductModelProductDescriptionAdvancedQuery, ProductModelProductDescriptionIdentifier>
{
    public ProductModelProductDescriptionRepository(SQLiteService sqLiteService) : base(sqLiteService)
    {
        _database.CreateTable<ProductModelProductDescriptionDataModel>();
    }

    protected override Expression<Func<ProductModelProductDescriptionDataModel, bool>> GetItemExpression(ProductModelProductDescriptionDataModel item)
    {
        return t => t.ProductModelID == item.ProductModelID&&t.ProductDescriptionID == item.ProductDescriptionID&&t.Culture == item.Culture;
    }

    protected override Expression<Func<ProductModelProductDescriptionDataModel, bool>> GetItemExpression(ProductModelProductDescriptionIdentifier identifier)
    {
        return t => t.ProductModelID == identifier.ProductModelID&&t.ProductDescriptionID == identifier.ProductDescriptionID&&t.Culture == identifier.Culture;
    }

    /// <summary>
    /// this expression will be used in Search(TAdvancedQuery query) to filter data in SQLite table
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    protected override Expression<Func<ProductModelProductDescriptionDataModel, bool>> GetSQLiteTableQueryPredicateByAdvancedQuery(ProductModelProductDescriptionAdvancedQuery query)
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

