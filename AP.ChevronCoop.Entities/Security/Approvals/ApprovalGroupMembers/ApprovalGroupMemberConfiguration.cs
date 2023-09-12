using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers
{

    public partial class ApprovalGroupMemberConfiguration : BaseEntityConfiguration<ApprovalGroupMember, string>
    {
        public override void Configure(EntityTypeBuilder<ApprovalGroupMember> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(ApprovalGroupMember), DbSchemaConstants.Security);
            // entity.HasIndex(x => new { x.ApprovalGroupId });


            entity.Property(e => e.ApprovalGroupId);
            entity.HasOne(e => e.ApprovalGroup)
                .WithMany(g => g.GroupMembers)
                .HasForeignKey(e => e.ApprovalGroupId)
                .IsRequired();

            // entity.HasIndex(x => new { x.ApplicationUserId });
            entity.Property(e => e.ApplicationUserId);
            entity.HasOne(e => e.ApplicationUser)
                .WithMany()
                .HasForeignKey(e => e.ApplicationUserId)
                .IsRequired();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ApprovalGroupMember> entity);
    }

}