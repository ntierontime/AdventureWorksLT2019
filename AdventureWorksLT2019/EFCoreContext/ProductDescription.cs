using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class ProductDescription
    {
        public ProductDescription()
        {
            this.ProductModelProductDescription = new HashSet<ProductModelProductDescription>();

        }
        public int ProductDescriptionID { get; set; }

        public string Description { get; set; } = null!;

        public System.Guid rowguid { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public ICollection<ProductModelProductDescription> ProductModelProductDescription { get; set; }

    }
}

