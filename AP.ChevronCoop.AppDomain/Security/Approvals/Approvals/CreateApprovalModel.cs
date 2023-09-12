using AP.ChevronCoop.Entities;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;

public class CreateApprovalModel
{
	public string Module { get; set; }
	public ApprovalType ApprovalType { get; set; }
	public string Payload { get; set; }
	public string CreatedBy { get; set; }
	public string Description { get; set; }
	public string? Comment { get; set; }
    public string EntityId { get; set; }
    public Type EntityType { get; set; }
    public string? ApprovalViewModelPayload { get; set; }
}