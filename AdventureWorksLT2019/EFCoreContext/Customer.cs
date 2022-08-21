using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class Customer
    {
        public Customer()
        {
            this.CustomerAddress = new HashSet<CustomerAddress>();
            this.SalesOrderHeader = new HashSet<SalesOrderHeader>();

        }
        public int CustomerID { get; set; }

        public bool NameStyle { get; set; }

        public string? Title { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public string? Suffix { get; set; }

        public string? CompanyName { get; set; }

        public string? SalesPerson { get; set; }

        public string? EmailAddress { get; set; }

        public string? Phone { get; set; }

        public string PasswordHash { get; set; } = null!;

        public string PasswordSalt { get; set; } = null!;

        public System.Guid rowguid { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public ICollection<CustomerAddress> CustomerAddress { get; set; }

        public ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }

    }
}

