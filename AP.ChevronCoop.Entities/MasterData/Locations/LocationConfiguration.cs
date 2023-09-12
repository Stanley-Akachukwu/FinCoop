using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.MasterData.Locations;

public partial class LocationConfiguration : BaseEntityConfiguration<Location, string>
{
    public override void Configure(EntityTypeBuilder<Location> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(Location), DbSchemaConstants.MasterData);

        entity.HasIndex(x => new { x.Name }).IsUnique();
        entity.HasIndex(x => new { x.Name, x.Code }).IsUnique();

        entity.Property(l => l.LocationType)
          .HasConversion<string>()
          .HasMaxLength(100)
          .IsRequired();

        entity.HasOne(l => l.Parent)
          .WithMany(p => p.Children)
          .HasForeignKey(l => l.ParentId);

        entity.Property(l => l.ParentId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.Property(l => l.Code)
          .HasColumnType("nvarchar")
          .HasMaxLength(64)
          .IsRequired();

        entity.Property(l => l.Name)
          .HasColumnType("nvarchar")
          .HasMaxLength(256)
          .IsRequired();

        entity.Property(l => l.SystemFlag)
          .IsRequired();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Location> entity);
}