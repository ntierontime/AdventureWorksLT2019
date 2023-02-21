using Framework.Models;
namespace AdventureWorksLT2019.Models
{
    public partial class SalesOrderDetailCompositeModel : CompositeModel<SalesOrderDetailDataModel.DefaultView, SalesOrderDetailCompositeModel.__DataOptions__>
    {
        // 2. AncestorTable = 2,
        public ProductDataModel.DefaultView? Product { get; set; }
        public SalesOrderHeaderDataModel.DefaultView? SalesOrderHeader { get; set; }

        public enum __DataOptions__
        {
            __Master__,
            // 2. AncestorTable
            Product,
            SalesOrderHeader,

        }
    }
}

