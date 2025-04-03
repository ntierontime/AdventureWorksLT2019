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
        public string Database_Version { get; set; } = null!;

        [Display(Name = "VersionDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime VersionDate { get; set; } = DateTime.Now;

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

