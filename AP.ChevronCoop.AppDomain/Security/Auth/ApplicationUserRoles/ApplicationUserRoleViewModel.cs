using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserRoles
{
    public partial class ApplicationUserRoleViewModel : BaseViewModel
    {

        [MaxLength(900)]
        [Required]
        public string UserId { get; set; }

        [MaxLength(900)]
        [Required]
        public string RoleId { get; set; }

    }



}
