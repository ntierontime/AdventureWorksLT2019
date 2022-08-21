using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class ProductDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "ProductID", ResourceType = typeof(UIStrings))]
        public int ProductID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Name_should_be_1_to_50", MinimumLength = 1)]
        public string Name { get; set; } = null!;

        [Display(Name = "ProductNumber", ResourceType = typeof(UIStrings))]
        [StringLength(25, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_ProductNumber_should_be_1_to_25", MinimumLength = 1)]
        public string ProductNumber { get; set; } = null!;

        [Display(Name = "Color", ResourceType = typeof(UIStrings))]
        [StringLength(15, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Color_should_be_0_to_15")]
        public string? Color { get; set; }

        [Display(Name = "StandardCost", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="StandardCost_is_required")]
        public decimal StandardCost { get; set; }

        [Display(Name = "ListPrice", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ListPrice_is_required")]
        public decimal ListPrice { get; set; }

        [Display(Name = "Size", ResourceType = typeof(UIStrings))]
        [StringLength(5, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Size_should_be_0_to_5")]
        public string? Size { get; set; }

        [Display(Name = "Weight", ResourceType = typeof(UIStrings))]
        public decimal? Weight { get; set; }

        [Display(Name = "ProductCategory", ResourceType = typeof(UIStrings))]
        public int? ProductCategoryID { get; set; }

        [Display(Name = "ProductModel", ResourceType = typeof(UIStrings))]
        public int? ProductModelID { get; set; }

        [Display(Name = "SellStartDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="SellStartDate_is_required")]
        public System.DateTime SellStartDate { get; set; }

        [Display(Name = "SellEndDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime? SellEndDate { get; set; }

        [Display(Name = "DiscontinuedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime? DiscontinuedDate { get; set; }

        [Display(Name = "ThumbNailPhoto", ResourceType = typeof(UIStrings))]
        public System.Byte[]? ThumbNailPhoto { get; set; }

        [Display(Name = "ThumbnailPhotoFileName", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_ThumbnailPhotoFileName_should_be_0_to_50")]
        public string? ThumbnailPhotoFileName { get; set; }

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="rowguid_is_required")]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
        public System.DateTime ModifiedDate { get; set; }

        public partial class DefaultView: ProductDataModel
        {
            [Display(Name = "Name", ResourceType = typeof(UIStrings))]
            public string? ProductCategory_Name { get; set; }

            [Display(Name = "Name", ResourceType = typeof(UIStrings))]
            public string? ProductModel_Name { get; set; }
        }

    }
}

