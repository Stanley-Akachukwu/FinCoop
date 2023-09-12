using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.MasterData.GlobalCodes;

public partial class GlobalCodeConfiguration : BaseEntityConfiguration<GlobalCode, string>
{
    public override void Configure(EntityTypeBuilder<GlobalCode> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(GlobalCode), DbSchemaConstants.MasterData);

        //entity.HasIndex(x => new { x.Name }).IsUnique();
        entity.HasIndex(x => new { x.Name, x.Code, x.CodeType }).IsUnique();

        entity.Property(gc => gc.CodeType)
          .HasConversion<string>()
          .HasMaxLength(100)
          .IsRequired();

        entity.Property(gc => gc.Code)
          .HasColumnType("nvarchar")
          .HasMaxLength(64)
          .IsRequired();

        entity.Property(gc => gc.Name)
          .HasColumnType("nvarchar")
          .HasMaxLength(128)
          .IsRequired();

        entity.Property(gc => gc.SystemFlag)
          .IsRequired();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<GlobalCode> entity);
}