using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.MemberApprovalActions;

public class HandleProductApplicationApprovalCommand : IRequest<CommandResult<ProductApplicationApprovalViewModel>>
{
  // public MemberApprovalAction MemberApprovalAction { get; set; }
  public string ApprovalId { get; set; }
  public string CreatedByUserId { get; set; }
}