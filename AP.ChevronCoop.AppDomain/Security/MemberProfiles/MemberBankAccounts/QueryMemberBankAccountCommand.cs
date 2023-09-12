using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBankAccounts;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBankAccounts;

public class QueryMemberBankAccountCommand : IRequest<CommandResult<IQueryable<MemberBankAccount>>>
{
}