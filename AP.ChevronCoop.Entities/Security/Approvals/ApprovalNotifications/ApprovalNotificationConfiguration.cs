using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalNotifications
{

    public partial class ApprovalNotificationConfiguration : BaseEntityConfiguration<ApprovalNotification, string>
    {
        public override void Configure(EntityTypeBuilder<ApprovalNotification> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(ApprovalNotification), DbSchemaConstants.Security);
            entity.HasIndex(x => new { x.ApprovalWorkflowId });

            entity.HasOne(e => e.ApprovalWorkflow)
        .WithMany()
        .HasForeignKey(e => e.ApprovalWorkflowId);

            entity.Property(e => e.Reminder)
              .HasColumnType("nvarchar(max)") 
            .IsRequired();

            entity.Property(e => e.Escalation)
                .HasColumnType("nvarchar(max)") 
               .IsRequired();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ApprovalNotification> entity);
    }
}

