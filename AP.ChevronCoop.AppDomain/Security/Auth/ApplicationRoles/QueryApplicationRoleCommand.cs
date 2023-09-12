using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles
{
    public class QueryApplicationRoleCommand : IRequest<CommandResult<IQueryable<ApplicationRole>>>
    {

    }
}
