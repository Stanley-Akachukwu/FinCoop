using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Payroll;

public partial class PayrollDeductionScheduleItemConfiguration : BaseEntityConfiguration<PayrollDeductionScheduleItem, string>
{
    public override void Configure(EntityTypeBuilder<PayrollDeductionScheduleItem> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(PayrollDeductionScheduleItem), DbSchemaConstants.Payroll);


        entity.Property(pd => pd.BatchRefNo).IsRequired();

        entity.Property(pd => pd.MemberId).IsRequired();
        entity.Property(pd => pd.MemberName).IsRequired();
        entity.Property(pd => pd.AccountNo).IsRequired();

        entity.Property(pd => pd.Amount)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(pd => pd.PayrollCode)
          .HasColumnType("nvarchar")
          .HasMaxLength(32)
          .IsRequired();

        entity.Property(pd => pd.Narration)
          .HasColumnType("nvarchar")
          .HasMaxLength(240)
          .IsRequired();

        entity.Property(pd => pd.PayrollDate).IsRequired();
        entity.Property(pd => pd.AccountDueDate).IsRequired();
        entity.Property(pd => pd.CurrentStatus)
          .HasConversion<string>()
          .HasMaxLength(120)
          .IsRequired();


        entity.Property(pd => pd.PayrollDeductionScheduleId).HasMaxLength(40).IsRequired(false);

        entity.HasOne(pd => pd.PayrollDeductionSchedule)
          .WithMany(pd => pd.ScheduleItems)
          .HasForeignKey(pd => pd.PayrollDeductionScheduleId)
          .IsRequired(false);

        entity.Property(pd => pd.LoanRepaymentScheduleId).HasMaxLength(40).IsRequired(false);

        entity.HasOne(pd => pd.LoanRepaymentSchedule)
          .WithMany()
          .HasForeignKey(pd => pd.LoanRepaymentScheduleId)
          .IsRequired(false);

        entity.Property(pd => pd.SavingsAccountDeductionScheduleId).HasMaxLength(40).IsRequired(false);

        entity.HasOne(pd => pd.SavingsAccountDeductionSchedule)
          .WithMany()
          .HasForeignKey(pd => pd.SavingsAccountDeductionScheduleId)
          .IsRequired(false);

        //entity.Property(pd => pd.PayrollCronJobConfigId).HasMaxLength(40).IsRequired(false);

        //entity.HasOne(pd => pd.PayrollCronJobConfig)
        //  .WithMany()
        //  .HasForeignKey(pd => pd.PayrollCronJobConfigId)
        //  .IsRequired(false);


        entity.Property(pd => pd.SpecialDepositAccountDeductionScheduleId).HasMaxLength(40).IsRequired(false);

        entity.HasOne(pd => pd.SpecialDepositAccountDeductionSchedule)
          .WithMany()
          .HasForeignKey(pd => pd.SpecialDepositAccountDeductionScheduleId)
          .IsRequired(false);


        entity.Property(e => e.DeductionType).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<PayrollDeductionScheduleItem> entity);
}