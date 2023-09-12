using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins
{
    public partial class ApplicationUserLoginViewModel : BaseViewModel
    {

        [MaxLength(900)]
        [Required]
        public string LoginProvider { get; set; }

        [MaxLength(900)]
        [Required]
        public string ProviderKey { get; set; }

        public string? ProviderDisplayName { get; set; }

        [MaxLength(900)]
        [Required]
        public string UserId { get; set; }

    }



}
