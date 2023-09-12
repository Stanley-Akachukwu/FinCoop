using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserClaims
{
    public partial class CreateApplicationUserClaimCommand : CreateCommand, IRequest<CommandResult<ApplicationUserClaimViewModel>>
    {

        [MaxLength(80)]

        public string? PermissionId { get; set; }

        [MaxLength(900)]
        [Required]
        public string UserId { get; set; }


        public string? ClaimType { get; set; }


        public string? ClaimValue { get; set; }

    }






}
