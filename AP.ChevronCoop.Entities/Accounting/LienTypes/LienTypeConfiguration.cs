using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Accounting.LienTypes;

public partial class LienTypeConfiguration : BaseEntityConfiguration<LienType, string>
{
  public override void Configure(EntityTypeBuilder<LienType> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(LienType), DbSchemaConstants.Accounting);

    entity.HasIndex(x => new { x.Name }).IsUnique();

    entity.Property(e => e.Code)
      .HasColumnType("nvarchar")
      .HasMaxLength(50)
      .IsRequired();

    entity.Property(e => e.Name)
      .HasColumnType("nvarchar")
      .HasMaxLength(250)
      .IsRequired();

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<LienType> entity);
}