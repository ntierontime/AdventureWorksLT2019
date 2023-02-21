using Framework.Models;
namespace AdventureWorksLT2019.Models
{
    public partial class ProductCompositeModel : CompositeModel<ProductDataModel.DefaultView, ProductCompositeModel.__DataOptions__>
    {
        // 4. ListTable = 4,
        public SalesOrderDetailDataModel.DefaultView[]? SalesOrderDetails_Via_ProductID { get; set; }

        public enum __DataOptions__
        {
            __Master__,
            // 4. ListTable
            SalesOrderDetails_Via_ProductID,

        }
    }
}

