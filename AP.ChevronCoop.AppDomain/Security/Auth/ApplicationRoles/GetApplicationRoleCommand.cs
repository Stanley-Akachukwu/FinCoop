using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles;

public record GetApplicationRoleCommand(string Id): IRequest<CommandResult<GetApplicationRoleViewModel>>;