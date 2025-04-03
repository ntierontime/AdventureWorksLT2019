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
        public string AddressLine1 { get; set; } = null!;

        [Display(Name = "AddressLine2", ResourceType = typeof(UIStrings))]
        public string? AddressLine2 { get; set; }

        [Display(Name = "City", ResourceType = typeof(UIStrings))]
        public string City { get; set; } = null!;

        [Display(Name = "StateProvince", ResourceType = typeof(UIStrings))]
        public string StateProvince { get; set; } = null!;

        [Display(Name = "CountryRegion", ResourceType = typeof(UIStrings))]
        public string CountryRegion { get; set; } = null!;

        [Display(Name = "PostalCode", ResourceType = typeof(UIStrings))]
        public string PostalCode { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

