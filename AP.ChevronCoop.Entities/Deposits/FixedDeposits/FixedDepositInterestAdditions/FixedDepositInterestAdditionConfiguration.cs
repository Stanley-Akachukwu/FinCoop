using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;

public partial class
  FixedDepositInterestAdditionConfiguration : BaseEntityConfiguration<FixedDepositInterestAddition, string>
{
    public override void Configure(EntityTypeBuilder<FixedDepositInterestAddition> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(FixedDepositInterestAddition), DbSchemaConstants.Deposits);

        entity.Property(l => l.FixedDepositAccountId);
        entity.HasOne(e => e.FixedDepositAccount)
          .WithMany()
          .HasForeignKey(e => e.FixedDepositAccountId);


        entity.HasOne(e => e.InterestScheduleItem)
          .WithMany()
          .HasForeignKey(e => e.InterestScheduleItemId);

        entity.HasOne(e => e.TransactionJournal)
          .WithMany()
          .HasForeignKey(e => e.TransactionJournalId);

        entity.Property(l => l.InterestEarned)
          .IsRequired();


        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();


        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<FixedDepositInterestAddition> entity);
}