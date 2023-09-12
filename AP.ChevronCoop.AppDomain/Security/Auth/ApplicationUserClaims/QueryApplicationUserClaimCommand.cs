using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserClaims;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserClaims
{
    public class QueryApplicationUserClaimCommand : IRequest<CommandResult<IQueryable<ApplicationUserClaim>>>
    {

    }






}
