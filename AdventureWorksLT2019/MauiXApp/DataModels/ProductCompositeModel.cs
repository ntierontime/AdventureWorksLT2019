using Framework.Models;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public partial class ProductCompositeModel : CompositeModel<ProductDataModel, ProductCompositeModel.__DataOptions__>
{
        // 2. AncestorTable = 2,
        public ProductCategoryDataModel ProductCategory { get; set; }
        // 4. ListTable = 4,
        public SalesOrderDetailDataModel[] SalesOrderDetails_Via_ProductID { get; set; }

    public enum __DataOptions__
    {
        __Master__,
        // 2. AncestorTable
        ProductCategory,

        // 4. ListTable
        SalesOrderDetails_Via_ProductID,

    }
}

