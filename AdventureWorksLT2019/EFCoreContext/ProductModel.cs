using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class ProductModel
    {
        public ProductModel()
        {
            Product = new HashSet<Product>();
            ProductModelProductDescription = new HashSet<ProductModelProductDescription>();

        }
        public int ProductModelID { get; set; }

        public string Name { get; set; } = null!;

        public string? CatalogDescription { get; set; }

        public System.Guid rowguid { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public ICollection<Product> Product { get; set; }

        public ICollection<ProductModelProductDescription> ProductModelProductDescription { get; set; }

    }
}

