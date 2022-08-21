using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            this.Product = new HashSet<Product>();
            this.ProductCategory1 = new HashSet<ProductCategory>();

        }
        public int ProductCategoryID { get; set; }

        public int? ParentProductCategoryID { get; set; }

        public string Name { get; set; } = null!;

        public System.Guid rowguid { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public ProductCategory ProductCategory2 { get; set; } = null!;

        public ICollection<Product> Product { get; set; }

        public ICollection<ProductCategory> ProductCategory1 { get; set; }

    }
}

