using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
        {
        }

        public EFDbContext(DbContextOptions<EFDbContext> options)
            : base(options)
        {
        }

        public DbSet<BuildVersion> BuildVersion { get; set; } = null!;

        public DbSet<ErrorLog> ErrorLog { get; set; } = null!;

        public DbSet<Address> Address { get; set; } = null!;

        public DbSet<Customer> Customer { get; set; } = null!;

        public DbSet<CustomerAddress> CustomerAddress { get; set; } = null!;

        public DbSet<Product> Product { get; set; } = null!;

        public DbSet<ProductCategory> ProductCategory { get; set; } = null!;

        public DbSet<ProductDescription> ProductDescription { get; set; } = null!;

        public DbSet<ProductModel> ProductModel { get; set; } = null!;

        public DbSet<ProductModelProductDescription> ProductModelProductDescription { get; set; } = null!;

        public DbSet<SalesOrderDetail> SalesOrderDetail { get; set; } = null!;

        public DbSet<SalesOrderHeader> SalesOrderHeader { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=Localhost;Initial Catalog=AdventureWorksLT2019;UID=sa;PWD=abcd1234;", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region #1.1 AdventureWorksLT2019Model.BuildVersion

            modelBuilder.Entity<BuildVersion>(entity =>
            {

                entity.ToTable("BuildVersion", "dbo");

                entity.HasKey(e => new { e.SystemInformationID, e.VersionDate, e.ModifiedDate })
                    .HasName("PK_BuildVersion_SystemInformationID_VersionDate_ModifiedDate");

                entity.Property(e => e.SystemInformationID)
                    .HasColumnName("SystemInformationID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Database_Version)
                    .HasColumnName("[Database Version]")
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.VersionDate)
                    .HasColumnName("VersionDate")
                    .HasColumnType("DateTime")
                    .ValueGeneratedNever();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime")
                    .ValueGeneratedNever();

            });

            #endregion #1.1 AdventureWorksLT2019Model.BuildVersion

            #region #1.2 AdventureWorksLT2019Model.ErrorLog

            modelBuilder.Entity<ErrorLog>(entity =>
            {

                entity.ToTable("ErrorLog", "dbo");

                entity.HasKey(e => new { e.ErrorLogID })
                    .HasName("PK_ErrorLog_ErrorLogID");

                entity.Property(e => e.ErrorLogID)
                    .HasColumnName("ErrorLogID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ErrorTime)
                    .HasColumnName("ErrorTime")
                    .HasColumnType("DateTime");

                entity.Property(e => e.UserName)
                    .HasColumnName("UserName")
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ErrorNumber)
                    .HasColumnName("ErrorNumber");

                entity.Property(e => e.ErrorSeverity)
                    .HasColumnName("ErrorSeverity");

                entity.Property(e => e.ErrorState)
                    .HasColumnName("ErrorState");

                entity.Property(e => e.ErrorProcedure)
                    .HasColumnName("ErrorProcedure")
                    .HasMaxLength(126);

                entity.Property(e => e.ErrorLine)
                    .HasColumnName("ErrorLine");

                entity.Property(e => e.ErrorMessage)
                    .HasColumnName("ErrorMessage")
                    .IsRequired()
                    .HasMaxLength(4000);

            });

            #endregion #1.2 AdventureWorksLT2019Model.ErrorLog

            #region #1.3 AdventureWorksLT2019Model.Address

            modelBuilder.Entity<Address>(entity =>
            {

                entity.ToTable("Address", "SalesLT");

                entity.HasKey(e => new { e.AddressID })
                    .HasName("PK_Address_AddressID");

                entity.Property(e => e.AddressID)
                    .HasColumnName("AddressID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("AddressLine1")
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("AddressLine2")
                    .HasMaxLength(60);

                entity.Property(e => e.City)
                    .HasColumnName("City")
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.StateProvince)
                    .HasColumnName("StateProvince")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CountryRegion)
                    .HasColumnName("CountryRegion")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PostalCode)
                    .HasColumnName("PostalCode")
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.rowguid)
                    .HasColumnName("rowguid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime");

            });

            #endregion #1.3 AdventureWorksLT2019Model.Address

            #region #1.4 AdventureWorksLT2019Model.Customer

            modelBuilder.Entity<Customer>(entity =>
            {

                entity.ToTable("Customer", "SalesLT");

                entity.HasKey(e => new { e.CustomerID })
                    .HasName("PK_Customer_CustomerID");

                entity.Property(e => e.CustomerID)
                    .HasColumnName("CustomerID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NameStyle)
                    .HasColumnName("NameStyle");

                entity.Property(e => e.Title)
                    .HasColumnName("Title")
                    .HasMaxLength(8);

                entity.Property(e => e.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("MiddleName")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("LastName")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Suffix)
                    .HasColumnName("Suffix")
                    .HasMaxLength(10);

                entity.Property(e => e.CompanyName)
                    .HasColumnName("CompanyName")
                    .HasMaxLength(128);

                entity.Property(e => e.SalesPerson)
                    .HasColumnName("SalesPerson")
                    .HasMaxLength(256);

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("EmailAddress")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("Phone")
                    .HasMaxLength(25);

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("PasswordHash")
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PasswordSalt)
                    .HasColumnName("PasswordSalt")
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.rowguid)
                    .HasColumnName("rowguid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime");

            });

            #endregion #1.4 AdventureWorksLT2019Model.Customer

            #region #1.5 AdventureWorksLT2019Model.CustomerAddress

            modelBuilder.Entity<CustomerAddress>(entity =>
            {

                entity.ToTable("CustomerAddress", "SalesLT");

                entity.HasKey(e => new { e.CustomerID, e.AddressID })
                    .HasName("PK_CustomerAddress_CustomerID_AddressID");

                entity.Property(e => e.CustomerID)
                    .HasColumnName("CustomerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressID)
                    .HasColumnName("AddressID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressType)
                    .HasColumnName("AddressType")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.rowguid)
                    .HasColumnName("rowguid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.AddressID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            #endregion #1.5 AdventureWorksLT2019Model.CustomerAddress

            #region #1.6 AdventureWorksLT2019Model.Product

            modelBuilder.Entity<Product>(entity =>
            {

                entity.ToTable("Product", "SalesLT");

                entity.HasKey(e => new { e.ProductID })
                    .HasName("PK_Product_ProductID");

                entity.Property(e => e.ProductID)
                    .HasColumnName("ProductID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductNumber)
                    .HasColumnName("ProductNumber")
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Color)
                    .HasColumnName("Color")
                    .HasMaxLength(15);

                entity.Property(e => e.StandardCost)
                    .HasColumnName("StandardCost")
                    .HasColumnType("Money");

                entity.Property(e => e.ListPrice)
                    .HasColumnName("ListPrice")
                    .HasColumnType("Money");

                entity.Property(e => e.Size)
                    .HasColumnName("Size")
                    .HasMaxLength(5);

                entity.Property(e => e.Weight)
                    .HasColumnName("Weight");

                entity.Property(e => e.ProductCategoryID)
                    .HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductModelID)
                    .HasColumnName("ProductModelID");

                entity.Property(e => e.SellStartDate)
                    .HasColumnName("SellStartDate")
                    .HasColumnType("DateTime");

                entity.Property(e => e.SellEndDate)
                    .HasColumnName("SellEndDate")
                    .HasColumnType("DateTime");

                entity.Property(e => e.DiscontinuedDate)
                    .HasColumnName("DiscontinuedDate")
                    .HasColumnType("DateTime");

                entity.Property(e => e.ThumbNailPhoto)
                    .HasColumnName("ThumbNailPhoto");

                entity.Property(e => e.ThumbnailPhotoFileName)
                    .HasColumnName("ThumbnailPhotoFileName")
                    .HasMaxLength(50);

                entity.Property(e => e.rowguid)
                    .HasColumnName("rowguid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ProductModel)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductModelID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            #endregion #1.6 AdventureWorksLT2019Model.Product

            #region #1.7 AdventureWorksLT2019Model.ProductCategory

            modelBuilder.Entity<ProductCategory>(entity =>
            {

                entity.ToTable("ProductCategory", "SalesLT");

                entity.HasKey(e => new { e.ProductCategoryID })
                    .HasName("PK_ProductCategory_ProductCategoryID");

                entity.Property(e => e.ProductCategoryID)
                    .HasColumnName("ProductCategoryID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ParentProductCategoryID)
                    .HasColumnName("ParentProductCategoryID");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.rowguid)
                    .HasColumnName("rowguid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime");

                entity.HasOne(d => d.ProductCategory2)
                    .WithMany(p => p.ProductCategory1)
                    .HasForeignKey(d => d.ParentProductCategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            #endregion #1.7 AdventureWorksLT2019Model.ProductCategory

            #region #1.8 AdventureWorksLT2019Model.ProductDescription

            modelBuilder.Entity<ProductDescription>(entity =>
            {

                entity.ToTable("ProductDescription", "SalesLT");

                entity.HasKey(e => new { e.ProductDescriptionID })
                    .HasName("PK_ProductDescription_ProductDescriptionID");

                entity.Property(e => e.ProductDescriptionID)
                    .HasColumnName("ProductDescriptionID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.rowguid)
                    .HasColumnName("rowguid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime");

            });

            #endregion #1.8 AdventureWorksLT2019Model.ProductDescription

            #region #1.9 AdventureWorksLT2019Model.ProductModel

            modelBuilder.Entity<ProductModel>(entity =>
            {

                entity.ToTable("ProductModel", "SalesLT");

                entity.HasKey(e => new { e.ProductModelID })
                    .HasName("PK_ProductModel_ProductModelID");

                entity.Property(e => e.ProductModelID)
                    .HasColumnName("ProductModelID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CatalogDescription)
                    .HasColumnName("CatalogDescription")
                    .HasColumnType("Xml");

                entity.Property(e => e.rowguid)
                    .HasColumnName("rowguid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime");

            });

            #endregion #1.9 AdventureWorksLT2019Model.ProductModel

            #region #1.10 AdventureWorksLT2019Model.ProductModelProductDescription

            modelBuilder.Entity<ProductModelProductDescription>(entity =>
            {

                entity.ToTable("ProductModelProductDescription", "SalesLT");

                entity.HasKey(e => new { e.ProductModelID, e.ProductDescriptionID, e.Culture })
                    .HasName("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture");

                entity.Property(e => e.ProductModelID)
                    .HasColumnName("ProductModelID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductDescriptionID)
                    .HasColumnName("ProductDescriptionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Culture)
                    .HasColumnName("Culture")
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasMaxLength(6);

                entity.Property(e => e.rowguid)
                    .HasColumnName("rowguid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime");

                entity.HasOne(d => d.ProductDescription)
                    .WithMany(p => p.ProductModelProductDescription)
                    .HasForeignKey(d => d.ProductDescriptionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ProductModel)
                    .WithMany(p => p.ProductModelProductDescription)
                    .HasForeignKey(d => d.ProductModelID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            #endregion #1.10 AdventureWorksLT2019Model.ProductModelProductDescription

            #region #1.11 AdventureWorksLT2019Model.SalesOrderDetail

            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {

                entity.ToTable("SalesOrderDetail", "SalesLT");

                entity.HasKey(e => new { e.SalesOrderID, e.SalesOrderDetailID })
                    .HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

                entity.Property(e => e.SalesOrderID)
                    .HasColumnName("SalesOrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SalesOrderDetailID)
                    .HasColumnName("SalesOrderDetailID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.OrderQty)
                    .HasColumnName("OrderQty");

                entity.Property(e => e.ProductID)
                    .HasColumnName("ProductID");

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("UnitPrice")
                    .HasColumnType("Money");

                entity.Property(e => e.UnitPriceDiscount)
                    .HasColumnName("UnitPriceDiscount")
                    .HasColumnType("Money");

                entity.Property(e => e.LineTotal)
                    .HasColumnName("LineTotal")
                    .HasColumnType("Numeric(38,6)");

                entity.Property(e => e.rowguid)
                    .HasColumnName("rowguid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesOrderDetail)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.SalesOrderHeader)
                    .WithMany(p => p.SalesOrderDetail)
                    .HasForeignKey(d => d.SalesOrderID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            #endregion #1.11 AdventureWorksLT2019Model.SalesOrderDetail

            #region #1.12 AdventureWorksLT2019Model.SalesOrderHeader

            modelBuilder.Entity<SalesOrderHeader>(entity =>
            {

                entity.ToTable("SalesOrderHeader", "SalesLT");

                entity.HasKey(e => new { e.SalesOrderID })
                    .HasName("PK_SalesOrderHeader_SalesOrderID");

                entity.Property(e => e.SalesOrderID)
                    .HasColumnName("SalesOrderID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RevisionNumber)
                    .HasColumnName("RevisionNumber");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("OrderDate")
                    .HasColumnType("DateTime");

                entity.Property(e => e.DueDate)
                    .HasColumnName("DueDate")
                    .HasColumnType("DateTime");

                entity.Property(e => e.ShipDate)
                    .HasColumnName("ShipDate")
                    .HasColumnType("DateTime");

                entity.Property(e => e.Status)
                    .HasColumnName("Status");

                entity.Property(e => e.OnlineOrderFlag)
                    .HasColumnName("OnlineOrderFlag");

                entity.Property(e => e.SalesOrderNumber)
                    .HasColumnName("SalesOrderNumber")
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.PurchaseOrderNumber)
                    .HasColumnName("PurchaseOrderNumber")
                    .HasMaxLength(25);

                entity.Property(e => e.AccountNumber)
                    .HasColumnName("AccountNumber")
                    .HasMaxLength(15);

                entity.Property(e => e.CustomerID)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.ShipToAddressID)
                    .HasColumnName("ShipToAddressID");

                entity.Property(e => e.BillToAddressID)
                    .HasColumnName("BillToAddressID");

                entity.Property(e => e.ShipMethod)
                    .HasColumnName("ShipMethod")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreditCardApprovalCode)
                    .HasColumnName("CreditCardApprovalCode")
                    .HasMaxLength(15);

                entity.Property(e => e.SubTotal)
                    .HasColumnName("SubTotal")
                    .HasColumnType("Money");

                entity.Property(e => e.TaxAmt)
                    .HasColumnName("TaxAmt")
                    .HasColumnType("Money");

                entity.Property(e => e.Freight)
                    .HasColumnName("Freight")
                    .HasColumnType("Money");

                entity.Property(e => e.TotalDue)
                    .HasColumnName("TotalDue")
                    .HasColumnType("Money");

                entity.Property(e => e.Comment)
                    .HasColumnName("Comment")
                    .HasColumnType("NVarChar(MAX)");

                entity.Property(e => e.rowguid)
                    .HasColumnName("rowguid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("ModifiedDate")
                    .HasColumnType("DateTime");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.SalesOrderHeader)
                    .HasForeignKey(d => d.BillToAddressID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Address1)
                    .WithMany(p => p.SalesOrderHeader1)
                    .HasForeignKey(d => d.ShipToAddressID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesOrderHeader)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            #endregion #1.12 AdventureWorksLT2019Model.SalesOrderHeader

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        #region 1. GetAscendantOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory && GetDescendantOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory

/*

        public virtual ObjectResult<RecursivePathResultOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory> GetAscendantOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory(
            System.Int32 ProductCategoryID
            )
        {
            var ProductCategoryIDParameter = new ObjectParameter("ProductCategoryID", ProductCategoryID);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RecursivePathResultOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory>(
                "GetAscendantOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory"
                ,ProductCategoryIDParameter
                );
        }

        public virtual ObjectResult<RecursivePathResultOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory> GetDescendantOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory(
            System.Int32 ProductCategoryID
            )
        {
            var ProductCategoryIDParameter = new ObjectParameter("ProductCategoryID", ProductCategoryID);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RecursivePathResultOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory>(
                "GetDescendantOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory"
                , ProductCategoryIDParameter
                );
        }
*/
        #endregion 1. GetAscendantOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory && GetDescendantOfParentProductCategoryIDOfAdventureWorksLT2019Model_ProductCategory

    }
}

