using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.Permissions;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.Permissions
{
    public class QueryPermissionCommand : IRequest<CommandResult<IQueryable<Permission>>>
    {

    }






}
