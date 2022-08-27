using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class AddressDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "AddressID", ResourceType = typeof(UIStrings))]
        public int AddressID { get; set; }

        [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
        [StringLength(60, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_AddressLine1_should_be_1_to_60", MinimumLength = 1)]
        public string AddressLine1 { get; set; } = null!;

        [Display(Name = "AddressLine2", ResourceType = typeof(UIStrings))]
        [StringLength(60, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_AddressLine2_should_be_0_to_60")]
        public string? AddressLine2 { get; set; }

        [Display(Name = "City", ResourceType = typeof(UIStrings))]
        [StringLength(30, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_City_should_be_1_to_30", MinimumLength = 1)]
        public string City { get; set; } = null!;

        [Display(Name = "StateProvince", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_StateProvince_should_be_1_to_50", MinimumLength = 1)]
        public string StateProvince { get; set; } = null!;

        [Display(Name = "CountryRegion", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_CountryRegion_should_be_1_to_50", MinimumLength = 1)]
        public string CountryRegion { get; set; } = null!;

        [Display(Name = "PostalCode", ResourceType = typeof(UIStrings))]
        [StringLength(15, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_PostalCode_should_be_1_to_15", MinimumLength = 1)]
        public string PostalCode { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="rowguid_is_required")]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}

