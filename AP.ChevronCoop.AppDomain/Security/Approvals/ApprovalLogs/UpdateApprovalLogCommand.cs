using AP.ChevronCoop.AppDomain;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalLogs;
using AP.ChevronCoop.Commons;
using MediatR;

public class UpdateApprovalLogCommand : UpdateCommand, IRequest<CommandResult<ApprovalLogViewModel>>
{
  public string ApprovalId { get; set; }
  public int Sequence { get; set; }
  public string ApprovalGroupId { get; set; }
  public string ApprovedByUserId { get; set; }
  public DateTime DateApproved { get; set; }
  public string Status { get; set; }
  public string Comment { get; set; }
}