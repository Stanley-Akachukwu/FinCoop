using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions
{
    public partial class
  SpecialDepositInterestAdditionConfiguration : BaseEntityConfiguration<SpecialDepositInterestAddition, string>
    {
        public override void Configure(EntityTypeBuilder<SpecialDepositInterestAddition> entity)
        {
            base.Configure(entity);
            entity.ToTable(nameof(SpecialDepositInterestAddition), DbSchemaConstants.Deposits);

            entity.HasIndex(x => new { x.SpecialDepositAccountId });

            entity.HasOne(e => e.SpecialDepositAccount)
              .WithMany()
              .HasForeignKey(e => e.SpecialDepositAccountId);


            entity.HasOne(e => e.InterestScheduleItem)
              .WithMany()
              .HasForeignKey(e => e.InterestScheduleItemId);

            entity.HasOne(e => e.TransactionJournal)
              .WithMany()
              .HasForeignKey(e => e.TransactionJournalId);

            entity.Property(l => l.InterestEarned)
              .IsRequired();

            entity.Property(l => l.ProcessedDate)
              .IsRequired();

            entity.Property(l => l.ProcessedDate)
              .IsRequired();

            entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();


            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SpecialDepositInterestAddition> entity);
    }
}