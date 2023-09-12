using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;

public class QueryApprovalGroupCommand : IRequest<CommandResult<IQueryable<ApprovalGroup>>>
{
	
}