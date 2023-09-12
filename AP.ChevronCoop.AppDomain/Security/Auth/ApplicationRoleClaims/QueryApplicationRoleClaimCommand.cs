using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoleClaims
{
    public class QueryApplicationRoleClaimCommand : IRequest<CommandResult<IQueryable<ApplicationRoleClaim>>>
    {

    }






}
