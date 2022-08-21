using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class Product
    {
        public Product()
        {
            this.SalesOrderDetail = new HashSet<SalesOrderDetail>();

        }
        public int ProductID { get; set; }

        public string Name { get; set; } = null!;

        public string ProductNumber { get; set; } = null!;

        public string? Color { get; set; }

        public decimal StandardCost { get; set; }

        public decimal ListPrice { get; set; }

        public string? Size { get; set; }

        public decimal? Weight { get; set; }

        public int? ProductCategoryID { get; set; }

        public int? ProductModelID { get; set; }

        public System.DateTime SellStartDate { get; set; }

        public System.DateTime? SellEndDate { get; set; }

        public System.DateTime? DiscontinuedDate { get; set; }

        public System.Byte[]? ThumbNailPhoto { get; set; }

        public string? ThumbnailPhotoFileName { get; set; }

        public System.Guid rowguid { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public ProductCategory ProductCategory { get; set; } = null!;

        public ProductModel ProductModel { get; set; } = null!;

        public ICollection<SalesOrderDetail> SalesOrderDetail { get; set; }

    }
}

