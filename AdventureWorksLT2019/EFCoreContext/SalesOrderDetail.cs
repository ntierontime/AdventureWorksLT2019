using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class SalesOrderDetail
    {
        public SalesOrderDetail()
        {

        }
        public int SalesOrderID { get; set; }

        public int SalesOrderDetailID { get; set; }

        public short OrderQty { get; set; }

        public int ProductID { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitPriceDiscount { get; set; }

        public decimal LineTotal { get; set; }

        public System.Guid rowguid { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public Product Product { get; set; } = null!;

        public SalesOrderHeader SalesOrderHeader { get; set; } = null!;

    }
}

