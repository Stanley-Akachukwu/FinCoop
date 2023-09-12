using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.Approvals.Approvals;

public partial class ApprovalConfiguration: BaseEntityConfiguration<Approval, string>
{
  public override void Configure(EntityTypeBuilder<Approval> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(Approval), DbSchemaConstants.Security);
    
    entity.Property(e => e.ApprovalWorkflowId).IsRequired();
    entity.HasOne(e => e.ApprovalWorkflow)
      .WithMany()
      .HasForeignKey(e => e.ApprovalWorkflowId);
    
    entity.Property(e => e.CurrentSequence)
      .HasDefaultValue(0)
      .IsRequired();
    entity.Property(e => e.Payload).IsRequired();
    entity.Property(e => e.IsApprovalCompleted).IsRequired();

    entity.Property(t => t.EntityId).HasColumnType("nvarchar").HasMaxLength(100);
        entity.Property(t => t.Module).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
        entity.Property(t => t.TriedCount).HasDefaultValue(0).IsRequired();
    entity.Property(e => e.ApprovalType).HasMaxLength(200).HasConversion<string>();
    entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();
    
    entity.Property(e => e.Comment)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired(false);
    
    OnConfigurePartial(entity);
  }
  partial void OnConfigurePartial(EntityTypeBuilder<Approval> entity);
}