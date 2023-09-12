using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using Microsoft.AspNetCore.Identity;

namespace AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
