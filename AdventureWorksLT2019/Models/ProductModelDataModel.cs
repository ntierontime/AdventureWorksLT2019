using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class ProductModelDataModel
    {

        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "ProductModelID", ResourceType = typeof(UIStrings))]
        public int ProductModelID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(UIStrings))]
        public string Name { get; set; } = null!;

        [Display(Name = "CatalogDescription", ResourceType = typeof(UIStrings))]
        public string? CatalogDescription { get; set; }

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

