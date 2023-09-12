using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoleClaims
{
    public partial class ApplicationRoleClaimViewModel : BaseViewModel
    {
        [MaxLength(80)]
        public string PermissionId { get; set; }

        [MaxLength(900)]
        [Required]
        public string RoleId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

    }



}
