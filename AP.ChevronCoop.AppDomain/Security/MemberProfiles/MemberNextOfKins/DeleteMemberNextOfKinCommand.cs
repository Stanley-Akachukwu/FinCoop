using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberNextOfKins;

public class DeleteMemberNextOfKinCommand: DeleteCommand, IRequest<CommandResult<string>>
{
  
}