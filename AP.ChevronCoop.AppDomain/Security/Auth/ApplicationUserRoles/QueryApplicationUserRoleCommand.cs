using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserRoles
{
    public class QueryApplicationUserRoleCommand : IRequest<CommandResult<IQueryable<ApplicationUserRole>>>
    {

    }






}
