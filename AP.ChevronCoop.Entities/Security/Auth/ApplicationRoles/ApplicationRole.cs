using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using Microsoft.AspNetCore.Identity;

namespace AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles
{
    public class ApplicationRole : IdentityRole<string>
    {

        public ApplicationRole() : base()
        {
            //Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString();
        }


        public ApplicationRole(string roleName) : this()
        {
            Name = roleName;
        }
        
        public bool IsSystemRole { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
}
