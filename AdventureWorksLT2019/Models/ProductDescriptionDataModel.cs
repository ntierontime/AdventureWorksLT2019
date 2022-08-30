using AdventureWorksLT2019.Resx.Resources;
using Framework.Common;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class ProductDescriptionDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "ProductDescriptionID", ResourceType = typeof(UIStrings))]
        public int ProductDescriptionID { get; set; }

        [Display(Name = "Description", ResourceType = typeof(UIStrings))]
        [StringLength(400, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Description_should_be_1_to_400", MinimumLength = 1)]
        public string Description { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="rowguid_is_required")]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}

