using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Documents.OfficePhotos;

public partial class OfficePhotoConfiguration : BaseEntityConfiguration<OfficePhoto, string>
{
  public override void Configure(EntityTypeBuilder<OfficePhoto> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(OfficePhoto), DbSchemaConstants.Documents);

    entity.HasIndex(x => new { x.DocumentNo }).IsUnique();

    entity.Property(d => d.DocumentNo)
      .HasColumnType("nvarchar")
      .IsRequired()
      .HasMaxLength(32);

    entity.Property(d => d.DocumentTypeId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40)
      .IsRequired();

    entity.HasOne(d => d.DocumentType)
      .WithMany()
      .HasForeignKey(d => d.DocumentTypeId)
      .IsRequired();

    entity.Property(d => d.Name)
      .HasColumnType("nvarchar")
      .IsRequired()
      .HasMaxLength(128);

    entity.Property(d => d.Document)
      .HasColumnName("DocumentData");

    entity.Property(d => d.MimeType)
      .HasColumnType("nvarchar")
      .HasMaxLength(32);

    entity.Property(d => d.FilePath)
      .HasColumnType("nvarchar")
      .HasMaxLength(200);


    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<OfficePhoto> entity);
}