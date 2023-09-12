using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows
{

    public partial class ApprovalWorkflowConfiguration : BaseEntityConfiguration<ApprovalWorkflow, string>
    {
        public override void Configure(EntityTypeBuilder<ApprovalWorkflow> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(ApprovalWorkflow), DbSchemaConstants.Security);
            

            entity.Property(e => e.Description)
                  .HasColumnType("nvarchar").HasMaxLength(255)
                  .IsRequired(true);
            entity.Property(e => e.WorkflowName)
              .HasColumnType("nvarchar").HasMaxLength(80)
              .IsRequired(true);
           
            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ApprovalWorkflow> entity);
    }
}
