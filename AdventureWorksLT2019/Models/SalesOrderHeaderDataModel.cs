using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class SalesOrderHeaderDataModel
    {

        public partial class DefaultView: SalesOrderHeaderDataModel
        {
            [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
            public string? BillTo_Name { get; set; }

            [Display(Name = "Title", ResourceType = typeof(UIStrings))]
            public string? Customer_Name { get; set; }

            [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
            public string? ShipTo_Name { get; set; }
        }

        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "SalesOrderID", ResourceType = typeof(UIStrings))]
        public int SalesOrderID { get; set; }

        [Display(Name = "RevisionNumber", ResourceType = typeof(UIStrings))]
        public byte RevisionNumber { get; set; }

        [Display(Name = "OrderDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "DueDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime DueDate { get; set; } = DateTime.Now;

        [Display(Name = "ShipDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime? ShipDate { get; set; }

        [Display(Name = "Status", ResourceType = typeof(UIStrings))]
        public byte Status { get; set; }

        [Display(Name = "OnlineOrderFlag", ResourceType = typeof(UIStrings))]
        public bool OnlineOrderFlag { get; set; }

        [Display(Name = "SalesOrderNumber", ResourceType = typeof(UIStrings))]
        public string SalesOrderNumber { get; set; } = null!;

        [Display(Name = "PurchaseOrderNumber", ResourceType = typeof(UIStrings))]
        public string? PurchaseOrderNumber { get; set; }

        [Display(Name = "AccountNumber", ResourceType = typeof(UIStrings))]
        public string? AccountNumber { get; set; }

        [Display(Name = "Customer", ResourceType = typeof(UIStrings))]
        public int CustomerID { get; set; }

        [Display(Name = "Address", ResourceType = typeof(UIStrings))]
        public int? ShipToAddressID { get; set; }

        [Display(Name = "Address", ResourceType = typeof(UIStrings))]
        public int? BillToAddressID { get; set; }

        [Display(Name = "ShipMethod", ResourceType = typeof(UIStrings))]
        public string ShipMethod { get; set; } = null!;

        [Display(Name = "CreditCardApprovalCode", ResourceType = typeof(UIStrings))]
        public string? CreditCardApprovalCode { get; set; }

        [Display(Name = "SubTotal", ResourceType = typeof(UIStrings))]
        public decimal SubTotal { get; set; }

        [Display(Name = "TaxAmt", ResourceType = typeof(UIStrings))]
        public decimal TaxAmt { get; set; }

        [Display(Name = "Freight", ResourceType = typeof(UIStrings))]
        public decimal Freight { get; set; }

        [Display(Name = "TotalDue", ResourceType = typeof(UIStrings))]
        public decimal TotalDue { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(UIStrings))]
        public string? Comment { get; set; }

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

