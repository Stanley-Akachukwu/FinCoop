using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBeneficiaries;

public class DeleteMemberBeneficiaryCommand: DeleteCommand, IRequest<CommandResult<string>>
{
  
}