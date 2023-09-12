using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using MediatR;


namespace AP.ChevronCoop.AppDomain.Security.Approvals
{
    public class QueryApprovalCommand : IRequest<CommandResult<IQueryable<Approval>>>
    {
    }

    public record QueryUserApprovalCommand(string ApplicationUserId) : IRequest<CommandResult<IQueryable<ApprovalView>>>;
}
