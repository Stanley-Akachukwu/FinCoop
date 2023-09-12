using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;

public class ApprovalGroupMasterView
{
  public string Name { get; set; }

  [ConcurrencyCheck] public long? RowNumber { get; set; }

  public string Id { get; set; }
  public string Description { get; set; }
  public bool IsActive { get; set; }
  public string CreatedByUserId { get; set; }
  public DateTimeOffset? DateCreated { get; set; }
  public string UpdatedByUserId { get; set; }
  public DateTimeOffset? DateUpdated { get; set; }
  public Guid RowVersion { get; set; }
}