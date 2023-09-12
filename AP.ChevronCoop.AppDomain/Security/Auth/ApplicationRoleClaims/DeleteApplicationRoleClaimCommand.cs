using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoleClaims
{
    public partial class DeleteApplicationRoleClaimCommand : DeleteCommand, IRequest<CommandResult<string>>
    {
        //[Required]
        //public int RoleClaimId { get; set; }

    }






}
