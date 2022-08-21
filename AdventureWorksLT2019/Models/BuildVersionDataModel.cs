using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class BuildVersionDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "SystemInformationID", ResourceType = typeof(UIStrings))]
        public byte SystemInformationID { get; set; }

        [Display(Name = "Database_Version", ResourceType = typeof(UIStrings))]
        [StringLength(25, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Database_Version_should_be_1_to_25", MinimumLength = 1)]
        public string Database_Version { get; set; } = null!;

        [Display(Name = "VersionDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="VersionDate_is_required")]
        public System.DateTime VersionDate { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
        public System.DateTime ModifiedDate { get; set; }

    }
}

