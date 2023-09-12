using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins
{
    public partial class CreateApplicationUserLoginCommand : CreateCommand, IRequest<CommandResult<ApplicationUserLoginViewModel>>
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
