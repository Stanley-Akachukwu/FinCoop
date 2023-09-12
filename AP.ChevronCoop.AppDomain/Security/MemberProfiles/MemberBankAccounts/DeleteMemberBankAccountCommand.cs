using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBankAccounts;

public partial class DeleteMemberBankAccountCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}