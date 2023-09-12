using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserTokens
{
    public partial class ApplicationUserTokenViewModel : BaseViewModel
    {

        [MaxLength(900)]
        [Required]
        public string UserId { get; set; }

        [MaxLength(900)]
        [Required]
        public string LoginProvider { get; set; }

        [MaxLength(900)]
        [Required]
        public string Name { get; set; }

        public string? Value { get; set; }

    }



}
