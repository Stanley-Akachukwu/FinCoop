using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;

public partial class ApprovalGroupConfiguration : BaseEntityConfiguration<ApprovalGroup, string>
{
  public override void Configure(EntityTypeBuilder<ApprovalGroup> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(ApprovalGroup), DbSchemaConstants.Security);

    entity.Property(e => e.Name)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired();

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<ApprovalGroup> entity);
}