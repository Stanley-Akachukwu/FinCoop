using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;

public partial class ApprovalLogConfiguration: BaseEntityConfiguration<ApprovalLog, string>
{
  public override void Configure(EntityTypeBuilder<ApprovalLog> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(ApprovalLog), DbSchemaConstants.Security);
    
    entity.Property(e => e.ApprovalId).IsRequired();
    entity.HasOne(e => e.Approval)
      .WithMany()
      .HasForeignKey(e => e.ApprovalId);
    
    entity.Property(e => e.ApprovalGroupId).IsRequired(false);
    entity.HasOne(e => e.ApprovalGroup)
      .WithMany()
      .HasForeignKey(e => e.ApprovalGroupId);

    entity.Property(e => e.ApprovedByUserId).IsRequired();
    entity.HasOne(e => e.ApprovedByUser)
      .WithMany()
      .HasForeignKey(e => e.ApprovedByUserId);
    
    entity.Property(e => e.Sequence).IsRequired();
    entity.Property(e => e.DateApproved).IsRequired();

    entity.Property(e => e.Comment)
      .HasColumnType("nvarchar")
      .HasMaxLength(1024)
      .IsRequired(false);
    
    entity.Property(e => e.Status).HasMaxLength(32).HasConversion<string>();

    OnConfigurePartial(entity);
  }
  partial void OnConfigurePartial(EntityTypeBuilder<ApprovalLog> entity);
}



