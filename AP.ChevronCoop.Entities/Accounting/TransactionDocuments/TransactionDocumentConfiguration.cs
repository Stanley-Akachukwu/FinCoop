using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Accounting.TransactionDocuments;

public partial class TransactionDocumentConfiguration : BaseEntityConfiguration<TransactionDocument, string>
{
    public override void Configure(EntityTypeBuilder<TransactionDocument> entity)
    {
        base.Configure(entity);

        entity.HasIndex(d => d.DocumentNo).IsUnique();

        entity.ToTable(nameof(TransactionDocument), DbSchemaConstants.Accounting);

        entity.Property(d => d.DocumentNo)
          .HasMaxLength(32)
          .IsRequired();

        entity.Property(d => d.TransactionJournalId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40)
          .IsRequired();

        entity.HasOne(d => d.TransactionJournal)
          .WithMany(p => p.TransactionDocuments)
          .HasForeignKey(d => d.TransactionJournalId);

        entity.Property(d => d.DocumentTypeId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40)
          .IsRequired();

        entity.HasOne(d => d.DocumentType)
          .WithMany()
          .HasForeignKey(d => d.DocumentTypeId);

        entity.Property(d => d.Name)
          .HasColumnType("nvarchar")
          .HasMaxLength(128)
          .IsRequired();

        entity.Property(d => d.DocumentUrl)
          .HasColumnType("nvarchar")
          .HasMaxLength(2000);


        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<TransactionDocument> entity);
}