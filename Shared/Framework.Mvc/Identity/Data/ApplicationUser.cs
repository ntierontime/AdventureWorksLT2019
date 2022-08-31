using Microsoft.AspNetCore.Identity;

namespace Framework.Mvc.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //[PersonalData]
        //public long? EntityID { get; set; }
    }
}

