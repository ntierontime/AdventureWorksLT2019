using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class ProductModelProductDescription
    {
        public ProductModelProductDescription()
        {

        }
        public int ProductModelID { get; set; }

        public int ProductDescriptionID { get; set; }

        public string Culture { get; set; } = null!;

        public System.Guid rowguid { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public ProductDescription ProductDescription { get; set; } = null!;

        public ProductModel ProductModel { get; set; } = null!;

    }
}

