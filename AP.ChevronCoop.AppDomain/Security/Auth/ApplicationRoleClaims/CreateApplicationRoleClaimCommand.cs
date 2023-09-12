using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoleClaims
{
    public partial class CreateApplicationRoleClaimCommand : CreateCommand, IRequest<CommandResult<ApplicationRoleClaimViewModel>>
    {

        [MaxLength(80)]

        public string? PermissionId { get; set; }

        [MaxLength(900)]
        [Required]
        public string RoleId { get; set; }


        public string? ClaimType { get; set; }


        public string? ClaimValue { get; set; }

    }






}
