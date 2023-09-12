using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupWorkflows;

public partial class ApprovalGroupWorkflowConfiguration: BaseEntityConfiguration<ApprovalGroupWorkflow, string>
{
  public override void Configure(EntityTypeBuilder<ApprovalGroupWorkflow> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(ApprovalGroupWorkflow), DbSchemaConstants.Security);
            
            
    // Configure properties
    entity.Property(e => e.ApprovalWorkflowId).IsRequired();
    entity.HasOne(e => e.ApprovalWorkflow)
      .WithMany()
      .HasForeignKey(e => e.ApprovalWorkflowId);
    
    entity.Property(e => e.ApprovalGroupId).IsRequired();
    entity.HasOne(e => e.ApprovalGroup)
      .WithMany()
      .HasForeignKey(e => e.ApprovalGroupId);
            
    entity.Property(e => e.Sequence).IsRequired();
    entity.Property(e => e.RequiredApprovers).IsRequired();

    OnConfigurePartial(entity);
  }
  partial void OnConfigurePartial(EntityTypeBuilder<ApprovalGroupWorkflow> entity);
}