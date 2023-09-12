using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.MasterData.Departments;

public partial class DepartmentConfiguration : BaseEntityConfiguration<Department, string>
{
  public override void Configure(EntityTypeBuilder<Department> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(Department), DbSchemaConstants.MasterData);

    entity.HasIndex(x => x.Name).IsUnique();

    entity.Property(e => e.Name)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired();

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<Department> entity);
}