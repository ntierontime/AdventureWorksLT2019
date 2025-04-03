using AdventureWorksLT2019.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.Models
{
    public partial class ErrorLogDataModel
    {

        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "ErrorLogID", ResourceType = typeof(UIStrings))]
        public int ErrorLogID { get; set; }

        [Display(Name = "ErrorTime", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        public System.DateTime ErrorTime { get; set; } = DateTime.Now;

        [Display(Name = "UserName", ResourceType = typeof(UIStrings))]
        public string UserName { get; set; } = null!;

        [Display(Name = "ErrorNumber", ResourceType = typeof(UIStrings))]
        public int ErrorNumber { get; set; }

        [Display(Name = "ErrorSeverity", ResourceType = typeof(UIStrings))]
        public int? ErrorSeverity { get; set; }

        [Display(Name = "ErrorState", ResourceType = typeof(UIStrings))]
        public int? ErrorState { get; set; }

        [Display(Name = "ErrorProcedure", ResourceType = typeof(UIStrings))]
        public string? ErrorProcedure { get; set; }

        [Display(Name = "ErrorLine", ResourceType = typeof(UIStrings))]
        public int? ErrorLine { get; set; }

        [Display(Name = "ErrorMessage", ResourceType = typeof(UIStrings))]
        public string ErrorMessage { get; set; } = null!;
    }
}

