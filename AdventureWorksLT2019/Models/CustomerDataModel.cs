using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
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
        public bool NameStyle { get; set; }

        [Display(Name = "Title", ResourceType = typeof(UIStrings))]
        public string? Title { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(UIStrings))]
        public string FirstName { get; set; } = null!;

        [Display(Name = "MiddleName", ResourceType = typeof(UIStrings))]
        public string? MiddleName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(UIStrings))]
        public string LastName { get; set; } = null!;

        [Display(Name = "Suffix", ResourceType = typeof(UIStrings))]
        public string? Suffix { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(UIStrings))]
        public string? CompanyName { get; set; }

        [Display(Name = "SalesPerson", ResourceType = typeof(UIStrings))]
        public string? SalesPerson { get; set; }

        [Display(Name = "EmailAddress", ResourceType = typeof(UIStrings))]
        public string? EmailAddress { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(UIStrings))]
        public string? Phone { get; set; }

        [Display(Name = "PasswordHash", ResourceType = typeof(UIStrings))]
        public string PasswordHash { get; set; } = null!;

        [Display(Name = "PasswordSalt", ResourceType = typeof(UIStrings))]
        public string PasswordSalt { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

