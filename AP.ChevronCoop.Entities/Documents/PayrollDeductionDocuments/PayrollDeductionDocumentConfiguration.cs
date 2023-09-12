using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Documents.PayrollDeductionDocuments;

public partial class PayrollDeductionDocumentConfiguration: BaseEntityConfiguration<PayrollDeductionDocument, string>
{
  public override void Configure(EntityTypeBuilder<PayrollDeductionDocument> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(PayrollDeductionDocument), DbSchemaConstants.Documents);


    entity.Property(d => d.Document)
      .HasColumnName("DocumentData")
      .HasColumnType("nvarchar");

    entity.Property(d => d.FileName)
      .HasColumnType("nvarchar")
      .IsRequired()
      .HasMaxLength(128);

    entity.Property(d => d.Document)
      .HasColumnName("DocumentData");

    entity.Property(d => d.MimeType)
      .HasColumnType("nvarchar")
      .HasMaxLength(32);

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<PayrollDeductionDocument> entity);
}