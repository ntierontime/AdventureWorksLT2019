using AdventureWorksLT2019.Resx.Resources;
using Framework.Common;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class CustomerDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "CustomerID", ResourceType = typeof(UIStrings))]
        public int CustomerID { get; set; }

        [Display(Name = "NameStyle", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="NameStyle_is_required")]
        public bool NameStyle { get; set; }

        [Display(Name = "Title", ResourceType = typeof(UIStrings))]
        [StringLength(8, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Title_should_be_0_to_8")]
        public string? Title { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_FirstName_should_be_1_to_50", MinimumLength = 1)]
        public string FirstName { get; set; } = null!;

        [Display(Name = "MiddleName", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_MiddleName_should_be_0_to_50")]
        public string? MiddleName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_LastName_should_be_1_to_50", MinimumLength = 1)]
        public string LastName { get; set; } = null!;

        [Display(Name = "Suffix", ResourceType = typeof(UIStrings))]
        [StringLength(10, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Suffix_should_be_0_to_10")]
        public string? Suffix { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(UIStrings))]
        [StringLength(128, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_CompanyName_should_be_0_to_128")]
        public string? CompanyName { get; set; }

        [Display(Name = "SalesPerson", ResourceType = typeof(UIStrings))]
        [StringLength(256, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_SalesPerson_should_be_0_to_256")]
        public string? SalesPerson { get; set; }

        [Display(Name = "EmailAddress", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_EmailAddress_should_be_0_to_50")]
        public string? EmailAddress { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(UIStrings))]
        [StringLength(25, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Phone_should_be_0_to_25")]
        public string? Phone { get; set; }

        [Display(Name = "PasswordHash", ResourceType = typeof(UIStrings))]
        [StringLength(128, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_PasswordHash_should_be_1_to_128", MinimumLength = 1)]
        public string PasswordHash { get; set; } = null!;

        [Display(Name = "PasswordSalt", ResourceType = typeof(UIStrings))]
        [StringLength(10, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_PasswordSalt_should_be_1_to_10", MinimumLength = 1)]
        public string PasswordSalt { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="rowguid_is_required")]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}

