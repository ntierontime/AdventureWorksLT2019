using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class CustomerAddressDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "Customer", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="CustomerID_is_required")]
        public int CustomerID { get; set; }

        [Display(Name = "Address", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="AddressID_is_required")]
        public int AddressID { get; set; }

        [Display(Name = "AddressType", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_AddressType_should_be_1_to_50", MinimumLength = 1)]
        public string AddressType { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="rowguid_is_required")]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;

        public partial class DefaultView: CustomerAddressDataModel
        {
            [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
            public string? Address_Name { get; set; }

            [Display(Name = "Title", ResourceType = typeof(UIStrings))]
            public string? Customer_Name { get; set; }
        }

    }
}

