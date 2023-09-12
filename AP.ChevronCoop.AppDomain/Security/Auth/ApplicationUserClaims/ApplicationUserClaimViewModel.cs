using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserClaims
{
    public partial class ApplicationUserClaimViewModel : BaseViewModel
    {
        [MaxLength(80)]
        public string PermissionId { get; set; }

        [MaxLength(900)]
        [Required]
        public string UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

    }



}
