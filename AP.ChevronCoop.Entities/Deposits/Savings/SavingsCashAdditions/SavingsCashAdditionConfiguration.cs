using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;

public partial class SavingsCashAdditionConfiguration : BaseEntityConfiguration<SavingsCashAddition, string>
{
    public override void Configure(EntityTypeBuilder<SavingsCashAddition> entity)
    {
        base.Configure(entity);
        entity.ToTable(nameof(SavingsCashAddition), DbSchemaConstants.Deposits);

        entity.HasIndex(x => new { x.BatchRefNo }).IsUnique();
        entity.Property(e => e.BatchRefNo).HasColumnType("nvarchar").HasMaxLength(40);


        entity.Property(e => e.Amount)
          .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

        entity.HasOne(e => e.Approval)
            .WithMany()
            .HasForeignKey(e => e.ApprovalId)
            .IsRequired(false);

        entity.HasOne(e => e.TransactionJournal)
       .WithMany()
       .HasForeignKey(e => e.TransactionJournalId);

        entity.HasOne(e => e.SavingsAccount)
          .WithMany()
          .HasForeignKey(e => e.SavingsAccountId);


        entity.HasOne(e => e.CustomerPaymentDocument)
        .WithMany()
        .HasForeignKey(e => e.CustomerPaymentDocumentId);

        entity.Property(e => e.ModeOfPayment).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();


        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<SavingsCashAddition> entity);
}