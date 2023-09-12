using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.MemberApprovalActions
{
    public class QueryProductApplicationApprovalCommand : IRequest<CommandResult<ProductApplicationApprovalViewModel>>
    {
        public string ApprovalId { get; set; }
        public string CreatedByUserId { get; set; }
    }
}

