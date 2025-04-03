using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class CustomerAddressDataModel
    {

        public partial class DefaultView: CustomerAddressDataModel
        {
            [Display(Name = "AddressLine1", ResourceType = typeof(UIStrings))]
            public string? Address_Name { get; set; }

            [Display(Name = "Title", ResourceType = typeof(UIStrings))]
            public string? Customer_Name { get; set; }
        }

        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "Customer", ResourceType = typeof(UIStrings))]
        public int CustomerID { get; set; }

        [Display(Name = "Address", ResourceType = typeof(UIStrings))]
        public int AddressID { get; set; }

        [Display(Name = "AddressType", ResourceType = typeof(UIStrings))]
        public string AddressType { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

