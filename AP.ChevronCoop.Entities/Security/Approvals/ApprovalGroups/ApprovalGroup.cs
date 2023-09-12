using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;

public class ApprovalGroup : BaseEntity<string>
{
  public ApprovalGroup()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
    GroupMembers = new List<ApprovalGroupMember>();
  }

  public string Name { get; set; }
  public virtual List<ApprovalGroupMember> GroupMembers { get; set; }
  public override string DisplayCaption => "";
  public override string DropdownCaption => "";
  public override string ShortCaption => "";
}