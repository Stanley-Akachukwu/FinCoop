using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupWorkflows;

public class ApprovalGroupWorkflow: BaseEntity<string>
{
  public ApprovalGroupWorkflow()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
  }

  public string ApprovalWorkflowId { get; set; }
  public ApprovalWorkflow ApprovalWorkflow { get; set; }
  public string ApprovalGroupId { get; set; }
  public ApprovalGroup ApprovalGroup { get; set; }
  public int Sequence { get; set; }
  public int RequiredApprovers { get; set; }
  
  
  
  public override string DisplayCaption => "";
  public override string DropdownCaption => "";
  public override string ShortCaption => "";
}