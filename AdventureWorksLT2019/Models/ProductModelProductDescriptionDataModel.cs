using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class ProductModelProductDescriptionDataModel
    {

        public partial class DefaultView: ProductModelProductDescriptionDataModel
        {
            [Display(Name = "Description", ResourceType = typeof(UIStrings))]
            public string? ProductDescription_Name { get; set; }

            [Display(Name = "Name", ResourceType = typeof(UIStrings))]
            public string? ProductModel_Name { get; set; }
        }

        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "ProductModel", ResourceType = typeof(UIStrings))]
        public int ProductModelID { get; set; }

        [Display(Name = "ProductDescription", ResourceType = typeof(UIStrings))]
        public int ProductDescriptionID { get; set; }

        [Display(Name = "Culture_", ResourceType = typeof(UIStrings))]
        public string Culture { get; set; } = null!;

        [Display(Name = "rowguid", ResourceType = typeof(UIStrings))]
        public System.Guid rowguid { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

