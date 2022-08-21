using Framework.Models;
namespace AdventureWorksLT2019.Models
{
    public partial class ProductModelCompositeModel : CompositeModel<ProductModelDataModel, ProductModelCompositeModel.__DataOptions__>
    {
        // 4. ListTable = 4,
        public ProductDataModel.DefaultView[]? Products_Via_ProductModelID { get; set; }
        public ProductModelProductDescriptionDataModel.DefaultView[]? ProductModelProductDescriptions_Via_ProductModelID { get; set; }

        public enum __DataOptions__
        {
            __Master__,
            // 4. ListTable
            Products_Via_ProductModelID,
            ProductModelProductDescriptions_Via_ProductModelID,

        }
    }
}

