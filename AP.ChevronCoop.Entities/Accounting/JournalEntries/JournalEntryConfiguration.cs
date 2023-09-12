using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Accounting.JournalEntries;

public partial class JournalEntryConfiguration : BaseEntityConfiguration<JournalEntry, string>
{
    public override void Configure(EntityTypeBuilder<JournalEntry> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(JournalEntry), DbSchemaConstants.Accounting);

        entity.HasIndex(x => new { x.TransactionEntryNo }).IsUnique();
        entity.HasIndex(x => new { x.TransactionEntryNo, x.AccountId }).IsUnique();

        entity.Property(e => e.TransactionEntryNo)
          .HasColumnType("nvarchar")
          .HasMaxLength(32)
          .IsRequired();

        entity.Property(e => e.EntryType)
          .HasConversion<string>()
          .HasMaxLength(100)
          .IsRequired();

        entity.Property(e => e.DecimalPlaces)
          .IsRequired();

        entity.Property(e => e.Debit)
          .IsRequired();

        entity.Property(e => e.Credit).IsRequired();

        entity.Property(e => e.TransactionDate).IsRequired();

        entity.Property(e => e.Memo)
          .HasColumnType("nvarchar")
          .HasMaxLength(512);


        entity.Property(e => e.TransactionJournalId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40)
          .IsRequired();

        entity.HasOne(e => e.TransactionJournal)
          .WithMany(t => t.JournalEntries)
          .HasForeignKey(e => e.TransactionJournalId)
          .IsRequired();

        entity.Property(e => e.AccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40)
          .IsRequired();

        entity.HasOne(e => e.Account)
          .WithMany()
          .HasForeignKey(e => e.AccountId)
          .IsRequired();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<JournalEntry> entity);
}