using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class SalesOrderHeaderDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "SalesOrderID", ResourceType = typeof(UIStrings))]
        public int SalesOrderID { get; set; }

        [Display(Name = "RevisionNumber", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="RevisionNumber_is_required")]
        public byte RevisionNumber { get; set; }

        [Display(Name = "OrderDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="OrderDate_is_required")]
        public System.DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "DueDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="DueDate_is_required")]
        public System.DateTime DueDate { get; set; } = DateTime.Now;

        [Display(Name = "ShipDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime? ShipDate { get; set; }

        [Display(Name = "Status", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="Status_is_required")]
        public byte Status { get; set; }

        [Display(Name = "OnlineOrderFlag", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="OnlineOrderFlag_is_required")]
        public bool OnlineOrderFlag { get; set; }

        [Display(Name = "SalesOrderNumber", ResourceType = typeof(UIStrings))]
        [StringLength(25, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_SalesOrderNumber_should_be_0_to_25")]
        public string SalesOrderNumber { get; set; } = null!;

        [Display(Name = "PurchaseOrderNumber", ResourceType = typeof(UIStrings))]
        [StringLength(25, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_PurchaseOrderNumber_should_be_0_to_25")]
        public string? PurchaseOrderNumber { get; set; }

        [Display(Name = "AccountNumber", ResourceType = typeof(UIStrings))]
        [StringLength(15, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_AccountNumber_should_be_0_to_15")]
        public string? AccountNumber { get; set; }

        [Display(Name = "Customer", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="CustomerID_is_required")]
        public int CustomerID { get; set; }

        [Display(Name = "Address", ResourceType = typeof(UIStrings))]
        public int? ShipToAddressID { get; set; }

        [Display(Name = "Address", ResourceType = typeof(UIStrings))]
        public int? BillToAddressID { get; set; }

        [Display(Name = "ShipMethod", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_ShipMethod_should_be_1_to_50", MinimumLength = 1)]
        public string ShipMethod { get; set; } = null!;

        [Display(Name = "CreditCardApprovalCode", ResourceType = typeof(UIStrings))]
        [StringLength(15, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_CreditCardApprovalCode_should_be_0_to_15")]
        public string? CreditCardApprovalCode { get; set; }

        [Display(Name = "SubTotal", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="SubTotal_is_required")]
        public decimal SubTotal { get; set; }

        [Display(Name = "TaxAmt", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="TaxAmt_is_required")]
        public decimal TaxAmt { get; set; }

        [Display(Name = "Freight", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="Freight_is_required")]
        public decimal Freight { get; set; }

        [Display(Name = "TotalDue", ResourceType = typeof(UIStrings))]
        public decimal TotalDue { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(UIStrings))]
        [StringLength(4096, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Comment_should_be_0_to_MAX")]
        public string? Comment { get; set; }

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="rowguid_is_required")]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;

        public partial class DefaultView: SalesOrderHeaderDataModel
        {
            [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
            public string? BillTo_Name { get; set; }

            [Display(Name = "Title", ResourceType = typeof(UIStrings))]
            public string? Customer_Name { get; set; }

            [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
            public string? ShipTo_Name { get; set; }
        }

    }
}

