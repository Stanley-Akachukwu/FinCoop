using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserTokens;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserTokens
{
    public class QueryApplicationUserTokenCommand : IRequest<CommandResult<IQueryable<ApplicationUserToken>>>
    {

    }






}
