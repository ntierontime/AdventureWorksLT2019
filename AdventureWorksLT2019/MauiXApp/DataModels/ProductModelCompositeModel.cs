using Framework.Models;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public partial class ProductModelCompositeModel : CompositeModel<ProductModelDataModel, ProductModelCompositeModel.__DataOptions__>
{
        // 4. ListTable = 4,
        public ProductDataModel[] Products_Via_ProductModelID { get; set; }
        public ProductModelProductDescriptionDataModel[] ProductModelProductDescriptions_Via_ProductModelID { get; set; }

    public enum __DataOptions__
    {
        __Master__,
        // 4. ListTable
        Products_Via_ProductModelID,
        ProductModelProductDescriptions_Via_ProductModelID,

    }
}

