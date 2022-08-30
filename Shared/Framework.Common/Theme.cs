using System.ComponentModel.DataAnnotations;

namespace Framework.Common
{
    public enum Theme
    {
        [Display(Name = "Light", ResourceType = typeof(Framework.Common.Resources.GlobalUIStrings))]
        Light,
        [Display(Name = "Dark", ResourceType = typeof(Framework.Common.Resources.GlobalUIStrings))]
        Dark
    }
}

