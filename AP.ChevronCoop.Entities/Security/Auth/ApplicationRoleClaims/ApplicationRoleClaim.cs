using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.Permissions;
using Microsoft.AspNetCore.Identity;

namespace AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public ApplicationRoleClaim()
        {
            // Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString();
        }
        public virtual ApplicationRole Role { get; set; }

        public string PermissionId { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public virtual Permission Permission { get; set; }

    }
}
