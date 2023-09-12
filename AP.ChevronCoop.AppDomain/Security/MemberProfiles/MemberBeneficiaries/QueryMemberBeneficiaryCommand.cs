using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBeneficiaries;

public class QueryMemberBeneficiaryCommand: IRequest<CommandResult<IQueryable<MemberBeneficiary>>>
{
  
}