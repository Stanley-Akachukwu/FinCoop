using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserTokens
{
    public partial class DeleteApplicationUserTokenCommand : DeleteCommand, IRequest<CommandResult<string>>
    {
        public string UserId { get; set; }
    }






}
