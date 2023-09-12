using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberNextOfKins;

public class QueryMemberNextOfKinCommand : IRequest<CommandResult<IQueryable<MemberNextOfKin>>>
{
  
}