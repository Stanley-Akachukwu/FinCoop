using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using Microsoft.AspNetCore.Identity;

namespace AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
