using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals;

public class HandleApprovalCommand :  IRequest<CommandResult<string>>
{
	public string ApprovalId { get; set; }
	public string Comment { get; set; }
	public ApprovalStatus Status { get; set; }
	public string ApplicationUserId { get; set; }
}