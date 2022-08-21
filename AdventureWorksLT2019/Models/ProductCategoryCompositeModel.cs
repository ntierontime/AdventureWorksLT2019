using Framework.Models;
namespace AdventureWorksLT2019.Models
{
    public partial class ProductCategoryCompositeModel : CompositeModel<ProductCategoryDataModel.DefaultView, ProductCategoryCompositeModel.__DataOptions__>
    {
        // 4. ListTable = 4,
        public ProductDataModel.DefaultView[]? Products_Via_ProductCategoryID { get; set; }
        public ProductCategoryDataModel.DefaultView[]? ProductCategories_Via_ParentProductCategoryID { get; set; }

        public enum __DataOptions__
        {
            __Master__,
            // 4. ListTable
            Products_Via_ProductCategoryID,
            ProductCategories_Via_ParentProductCategoryID,

        }
    }
}

