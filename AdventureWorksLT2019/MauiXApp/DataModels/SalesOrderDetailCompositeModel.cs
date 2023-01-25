using Framework.Models;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public partial class SalesOrderDetailCompositeModel : CompositeModel<SalesOrderDetailDataModel, SalesOrderDetailCompositeModel.__DataOptions__>
{
        // 2. AncestorTable = 2,
        public ProductDataModel Product { get; set; }
        public ProductCategoryDataModel ProductCategory { get; set; }
        public SalesOrderHeaderDataModel SalesOrderHeader { get; set; }

    public enum __DataOptions__
    {
        __Master__,
        // 2. AncestorTable
        Product,
        ProductCategory,
        SalesOrderHeader,

    }
}

