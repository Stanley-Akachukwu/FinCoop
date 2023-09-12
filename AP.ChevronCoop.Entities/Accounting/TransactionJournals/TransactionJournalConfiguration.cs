using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Accounting.TransactionJournals;

public partial class TransactionJournalConfiguration : BaseEntityConfiguration<TransactionJournal, string>
{
    public override void Configure(EntityTypeBuilder<TransactionJournal> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(TransactionJournal), DbSchemaConstants.Accounting);

        entity.HasIndex(x => new { x.TransactionNo }).IsUnique();
        entity.HasIndex(x => new { x.TransactionNo, x.Title }).IsUnique();

        entity.Property(t => t.TransactionNo)
          .HasColumnType("nvarchar")
          .IsRequired()
          .HasMaxLength(50);

        entity.Property(t => t.Title)
          .HasColumnType("nvarchar")
          .IsRequired()
          .HasMaxLength(256);
        
        entity.Property(t => t.DocumentRef)
          .HasColumnType("nvarchar")
          .HasMaxLength(128);

        entity.Property(t => t.DocumentRefId)
     .HasColumnType("nvarchar")
     .HasMaxLength(128);

        entity.Property(t => t.PostingRef)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

        entity.Property(t => t.PostingRefId)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

        entity.Property(t => t.EntityRef)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

        entity.Property(t => t.EntityRefId)
     .HasColumnType("nvarchar")
     .HasMaxLength(128);

        entity.Property(t => t.TransactionDate)
      .IsRequired();

        entity.Property(t => t.IsPosted);

        entity.Property(t => t.PostedByUserId);
        entity.HasOne(e => e.PostedByUser)
          .WithMany()
          .HasForeignKey(e => e.PostedByUserId)
          .IsRequired(false);
        
        
        entity.Property(t => t.ApprovalId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);
        
        entity.HasOne(e => e.Approval)
          .WithMany()
          .HasForeignKey(e => e.ApprovalId)
          .IsRequired(false);

        entity.Property(t => t.DatePosted);

        entity.Property(t => t.IsReversed);

        entity.Property(t => t.ReversedByUserId);
        entity.HasOne(e => e.ReversedByUser)
          .WithMany()
          .HasForeignKey(e => e.ReversedByUserId)
          .IsRequired(false);

        entity.Property(t => t.ReversalDate);

        entity.Property(t => t.Memo)
          .HasColumnType("nvarchar")
          .HasMaxLength(512);
        
        entity.Property(e => e.TransactionType).HasDefaultValue(TransactionType.GENERAL_TRANSACTION).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<TransactionJournal> entity);
}