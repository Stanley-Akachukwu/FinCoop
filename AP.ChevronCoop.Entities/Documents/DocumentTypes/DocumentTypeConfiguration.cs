using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Documents.DocumentTypes;

public partial class DocumentTypeConfiguration : BaseEntityConfiguration<DocumentType, string>
{
  public override void Configure(EntityTypeBuilder<DocumentType> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(DocumentType), DbSchemaConstants.Documents);

    entity.HasIndex(e => e.Name).IsUnique();

    entity.Property(e => e.Name)
      .HasColumnType("nvarchar")
      .IsRequired()
      .HasMaxLength(128);

    entity.Property(e => e.SystemFlag)
      .IsRequired();

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<DocumentType> entity);
}