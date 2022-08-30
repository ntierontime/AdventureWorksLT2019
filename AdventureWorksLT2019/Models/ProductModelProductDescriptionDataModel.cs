using AdventureWorksLT2019.Resx.Resources;
using Framework.Common;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class ProductModelProductDescriptionDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "ProductModel", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ProductModelID_is_required")]
        public int ProductModelID { get; set; }

        [Display(Name = "ProductDescription", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ProductDescriptionID_is_required")]
        public int ProductDescriptionID { get; set; }

        [Display(Name = "Culture", ResourceType = typeof(UIStrings))]
        [StringLength(6, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Culture_should_be_1_to_6", MinimumLength = 1)]
        public string Culture { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="rowguid_is_required")]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;

        public partial class DefaultView: ProductModelProductDescriptionDataModel
        {
            [Display(Name = "Description", ResourceType = typeof(UIStrings))]
            public string? ProductDescription_Name { get; set; }

            [Display(Name = "Name", ResourceType = typeof(UIStrings))]
            public string? ProductModel_Name { get; set; }
        }

    }
}

