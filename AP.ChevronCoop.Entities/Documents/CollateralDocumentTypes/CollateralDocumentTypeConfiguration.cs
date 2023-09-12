using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Documents.CollateralDocumentTypes;

public partial class CollateralDocumentTypeConfiguration : BaseEntityConfiguration<CollateralDocumentType, string>
{
  public override void Configure(EntityTypeBuilder<CollateralDocumentType> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(CollateralDocumentType), DbSchemaConstants.Documents);
    
    entity.HasIndex(x => new { x.Name }).IsUnique();
    entity.HasIndex(x => new { x.Name, x.Code }).IsUnique();

    entity.Property(p => p.Code)
      .HasColumnType("nvarchar")
      .HasMaxLength(64)
      .IsRequired();
    entity.Property(p => p.Name)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired();
    entity.Property(p => p.SystemFlag).IsRequired();
    
    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<CollateralDocumentType> entity);
}