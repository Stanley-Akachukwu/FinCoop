using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.Permissions
{
    public partial class DeletePermissionCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }






}
