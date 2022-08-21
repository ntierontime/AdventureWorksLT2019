using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class Address
    {
        public Address()
        {
            this.CustomerAddress = new HashSet<CustomerAddress>();
            this.SalesOrderHeader = new HashSet<SalesOrderHeader>();
            this.SalesOrderHeader1 = new HashSet<SalesOrderHeader>();

        }
        public int AddressID { get; set; }

        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }

        public string City { get; set; } = null!;

        public string StateProvince { get; set; } = null!;

        public string CountryRegion { get; set; } = null!;

        public string PostalCode { get; set; } = null!;

        public System.Guid rowguid { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public ICollection<CustomerAddress> CustomerAddress { get; set; }

        public ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }

        public ICollection<SalesOrderHeader> SalesOrderHeader1 { get; set; }

    }
}

