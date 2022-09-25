using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class ProductCategoryDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "ProductCategoryID", ResourceType = typeof(UIStrings))]
        public int ProductCategoryID { get; set; }

        [Display(Name = "ProductCategory", ResourceType = typeof(UIStrings))]
        public int? ParentProductCategoryID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Name_should_be_1_to_50", MinimumLength = 1)]
        public string Name { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="rowguid_is_required")]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;

        public partial class DefaultView: ProductCategoryDataModel
        {
            [Display(Name = "ProductCategory", ResourceType = typeof(UIStrings))]
            public string? Parent_Name { get; set; }
        }

        public partial class DefaultWithPath
        {
            [Display(Name = "RecursivePath__", ResourceType = typeof(UIStrings))]
            public string RecursivePath__ { get; set; } = null!;
        }

    }
}

