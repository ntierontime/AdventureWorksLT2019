using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class ProductCategoryDataModel
    {

        public partial class DefaultView: ProductCategoryDataModel
        {
            [Display(Name = "Name", ResourceType = typeof(UIStrings))]
            public string? Parent_Name { get; set; }
        }

        public partial class DefaultWithPath
        {
            [Display(Name = "RecursivePath__", ResourceType = typeof(UIStrings))]
            public string RecursivePath__ { get; set; } = null!;
        }

        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "ProductCategoryID", ResourceType = typeof(UIStrings))]
        public int ProductCategoryID { get; set; }

        [Display(Name = "ProductCategory", ResourceType = typeof(UIStrings))]
        public int? ParentProductCategoryID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(UIStrings))]
        public string Name { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

