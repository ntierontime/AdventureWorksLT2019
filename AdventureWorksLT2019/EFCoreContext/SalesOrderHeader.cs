using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class SalesOrderHeader
    {
        public SalesOrderHeader()
        {
            this.SalesOrderDetail = new HashSet<SalesOrderDetail>();

        }
        public int SalesOrderID { get; set; }

        public byte RevisionNumber { get; set; }

        public System.DateTime OrderDate { get; set; }

        public System.DateTime DueDate { get; set; }

        public System.DateTime? ShipDate { get; set; }

        public byte Status { get; set; }

        public bool OnlineOrderFlag { get; set; }

        public string SalesOrderNumber { get; set; } = null!;

        public string? PurchaseOrderNumber { get; set; }

        public string? AccountNumber { get; set; }

        public int CustomerID { get; set; }

        public int? ShipToAddressID { get; set; }

        public int? BillToAddressID { get; set; }

        public string ShipMethod { get; set; } = null!;

        public string? CreditCardApprovalCode { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public string? Comment { get; set; }

        public System.Guid rowguid { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public Address Address { get; set; } = null!;

        public Address Address1 { get; set; } = null!;

        public Customer Customer { get; set; } = null!;

        public ICollection<SalesOrderDetail> SalesOrderDetail { get; set; }

    }
}

