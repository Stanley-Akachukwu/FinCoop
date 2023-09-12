using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers
{
    public class QueryApplicationUserCommand : IRequest<CommandResult<IQueryable<ApplicationUser>>>
    {

    }






}
