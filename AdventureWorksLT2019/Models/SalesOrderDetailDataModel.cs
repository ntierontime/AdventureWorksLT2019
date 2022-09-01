using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class SalesOrderDetailDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "SalesOrderHeader", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="SalesOrderID_is_required")]
        public int SalesOrderID { get; set; }

        [Display(Name = "SalesOrderDetailID", ResourceType = typeof(UIStrings))]
        public int SalesOrderDetailID { get; set; }

        [Display(Name = "OrderQty", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="OrderQty_is_required")]
        public short OrderQty { get; set; }

        [Display(Name = "Product", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ProductID_is_required")]
        public int ProductID { get; set; }

        [Display(Name = "UnitPrice", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="UnitPrice_is_required")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "UnitPriceDiscount", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="UnitPriceDiscount_is_required")]
        public decimal UnitPriceDiscount { get; set; }

        [Display(Name = "LineTotal", ResourceType = typeof(UIStrings))]
        public decimal LineTotal { get; set; }

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="rowguid_is_required")]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;

        public partial class DefaultView: SalesOrderDetailDataModel
        {
            [Display(Name = "Name", ResourceType = typeof(UIStrings))]
            public string? Product_Name { get; set; }

            [Display(Name = "SalesOrderNumber", ResourceType = typeof(UIStrings))]
            public string? SalesOrderHeader_Name { get; set; }
        }

    }
}

