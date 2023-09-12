using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins
{
    public class QueryApplicationUserLoginCommand : IRequest<CommandResult<IQueryable<ApplicationUserLogin>>>
    {

    }






}
