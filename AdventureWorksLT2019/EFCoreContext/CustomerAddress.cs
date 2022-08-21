using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class CustomerAddress
    {
        public CustomerAddress()
        {

        }
        public int CustomerID { get; set; }

        public int AddressID { get; set; }

        public string AddressType { get; set; } = null!;

        public System.Guid rowguid { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public Address Address { get; set; } = null!;

        public Customer Customer { get; set; } = null!;

    }
}

