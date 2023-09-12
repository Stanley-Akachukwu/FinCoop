using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserTokens
{
    public partial class UpdateApplicationUserTokenCommand : UpdateCommand, IRequest<CommandResult<ApplicationUserTokenViewModel>>
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
