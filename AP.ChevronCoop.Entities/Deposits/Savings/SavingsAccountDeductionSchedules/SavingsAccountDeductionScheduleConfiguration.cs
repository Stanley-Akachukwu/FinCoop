using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountDeductionSchedules;

public partial class SavingsAccountDeductionScheduleConfiguration : BaseEntityConfiguration<SavingsAccountDeductionSchedule, string>
{
    public override void Configure(EntityTypeBuilder<SavingsAccountDeductionSchedule> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(SavingsAccountDeductionSchedule), DbSchemaConstants.Deposits);



        entity.HasOne(e => e.SavingsAccount)
           .WithMany()
           .HasForeignKey(e => e.SavingsAccountId)
           .IsRequired();


        entity.Property(e => e.AccountNo)
          .HasMaxLength(50);

        entity.Property(e => e.BatchRefNo)
        .HasMaxLength(50);

        entity.Property(e => e.BatchRefNo)
    .HasMaxLength(50);
        entity.Property(e => e.MemberId)
          .HasMaxLength(60);

        entity.Property(e => e.MemberName)
       .HasMaxLength(100);

        entity.Property(e => e.EmployeeNo)
          .HasMaxLength(60);

        entity.Property(e => e.PayrollCode)
          .HasMaxLength(60);

        entity.Property(e => e.Narration)
          .HasMaxLength(100);

        entity.Property(e => e.CurrentStatus)
          .HasMaxLength(20);

        entity.Property(e => e.Amount)
       .HasPrecision(18, 3);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<SavingsAccountDeductionSchedule> entity);
}