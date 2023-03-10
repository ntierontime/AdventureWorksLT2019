using Framework.Models;
namespace AdventureWorksLT2019.Models
{
    public partial class ProductModelCompareModel
    {
        public ProductModelCompositeModel[]? ProductModelCompositeModelList { get; set; }
        // 4. ListTable = 4,

        /// <summary>
        /// key is the value of compared column, boolean value(true/false) means availability for each CompositeModel
        /// </summary>
        public Dictionary<string, bool[]>? CompareResult_Products_Via_ProductModelID { get; set; }

        public Dictionary<string, bool[]>? CompareResult_ProductModelProductDescriptions_Via_ProductModelID { get; set; }
    }

}

