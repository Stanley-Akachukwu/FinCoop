using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems
{
    public partial class
 SpecialDepositInterestScheduleItemConfiguration : BaseEntityConfiguration<SpecialDepositInterestScheduleItem, string>
    {
        public override void Configure(EntityTypeBuilder<SpecialDepositInterestScheduleItem> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(SpecialDepositInterestScheduleItem), DbSchemaConstants.Deposits);


            entity.HasOne(e => e.SpecialDepositInterestSchedule)
              .WithMany(e => e.ScheduleItems)
              .HasForeignKey(e => e.SpecialDepositInterestScheduleId);

            entity.HasOne(e => e.SpecialDepositAccount)
              .WithMany()
              .HasForeignKey(e => e.SpecialDepositAccountId);

            entity.Property(e => e.OldBalance)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            entity.Property(e => e.PeriodCashAddition)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            entity.Property(e => e.InterestRate)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            entity.Property(e => e.InterestEarned)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            entity.Property(e => e.NewBalance)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            // entity.Property(l => l.PaymentMode)
            //   .IsRequired();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SpecialDepositInterestScheduleItem> entity);
    }
}