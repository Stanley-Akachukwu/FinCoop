using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public partial class
  FixedDepositInterestScheduleItemConfiguration : BaseEntityConfiguration<FixedDepositInterestScheduleItem, string>
{
  public override void Configure(EntityTypeBuilder<FixedDepositInterestScheduleItem> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(FixedDepositInterestScheduleItem), DbSchemaConstants.Deposits);


    entity.HasIndex(x => new { x.FixedDepositAccountId });

    entity.HasOne(e => e.FixedDepositAccount)
      .WithMany()
      .HasForeignKey(e => e.FixedDepositAccountId);

    entity.Property(e => e.FixedDepositInterestScheduleId);
    entity.HasOne(e => e.FixedDepositInterestSchedule)
      .WithMany(e => e.ScheduleItems)
      .HasForeignKey(e => e.FixedDepositInterestScheduleId);

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

  partial void OnConfigurePartial(EntityTypeBuilder<FixedDepositInterestScheduleItem> entity);
}