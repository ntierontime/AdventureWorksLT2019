using Framework.Models;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public partial class ProductCategoryCompositeModel : CompositeModel<ProductCategoryDataModel, ProductCategoryCompositeModel.__DataOptions__>
{
        // 4. ListTable = 4,
        public ProductDataModel[] Products_Via_ProductCategoryID { get; set; }
        public ProductCategoryDataModel[] ProductCategories_Via_ParentProductCategoryID { get; set; }

    public enum __DataOptions__
    {
        __Master__,
        // 4. ListTable
        Products_Via_ProductCategoryID,
        ProductCategories_Via_ParentProductCategoryID,

    }
}

