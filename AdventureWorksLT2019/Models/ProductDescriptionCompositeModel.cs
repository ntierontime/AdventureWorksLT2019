using Framework.Models;
namespace AdventureWorksLT2019.Models
{
    public partial class ProductDescriptionCompositeModel : CompositeModel<ProductDescriptionDataModel, ProductDescriptionCompositeModel.__DataOptions__>
    {
        // 4. ListTable = 4,
        public ProductModelProductDescriptionDataModel.DefaultView[]? ProductModelProductDescriptions_Via_ProductDescriptionID { get; set; }

        public enum __DataOptions__
        {
            __Master__,
            // 4. ListTable
            ProductModelProductDescriptions_Via_ProductDescriptionID,

        }
    }
}

