using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;

public partial class PayrollDeductionScheduleConfiguration : BaseEntityConfiguration<PayrollDeductionSchedule, string>
{
    public override void Configure(EntityTypeBuilder<PayrollDeductionSchedule> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(PayrollDeductionSchedule), DbSchemaConstants.Payroll);


        entity.HasIndex(x => new { x.ScheduleName }).IsUnique();

        entity.Property(pds => pds.ScheduleName)
          .HasColumnType("nvarchar").HasMaxLength(256)
          .IsRequired();


        entity.Property(pds => pds.BankAccountId)
          .HasColumnType("nvarchar");


        entity.HasOne(pds => pds.BankAccount)
          .WithMany()
          .HasForeignKey(pds => pds.BankAccountId)
          .IsRequired(false);


        entity.Property(pds => pds.SpecialDepositBankAccountId)
         .HasColumnType("nvarchar");

        entity.HasOne(pds => pds.SpecialDepositBankAccount)
          .WithMany()
          .HasForeignKey(pds => pds.SpecialDepositBankAccountId)
          .IsRequired(false);

        entity.Property(pds => pds.FixedDepositBankAccountId)
         .HasColumnType("nvarchar");

        entity.HasOne(pds => pds.FixedDepositBankAccount)
          .WithMany()
          .HasForeignKey(pds => pds.FixedDepositBankAccountId)
          .IsRequired(false);


        entity.Property(pds => pds.TotalDeductions)
      .HasPrecision(18, 2)
      .HasDefaultValue(0.00)
      .IsRequired();

        entity.Property(pds => pds.MinDecimalPlace).HasDefaultValue(1);
        entity.Property(pds => pds.MaxDecimalPlace).HasDefaultValue(1);

        entity.Property(pds => pds.AdviseDate).IsRequired();
        entity.Property(pds => pds.ExpectedDate).IsRequired();

        entity.Property(pds => pds.IsPosted).IsRequired();
        entity.Property(pds => pds.PayrollDate);

        entity.Property(pds => pds.IsUploaded).IsRequired();
        entity.Property(pds => pds.LastUploadedDate);

        entity.Property(pds => pds.IsProcessed).IsRequired();
        entity.Property(pds => pds.ProcessedDate);

        entity.Property(e => e.ScheduleType).HasMaxLength(100).HasConversion<string>();
        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<PayrollDeductionSchedule> entity);
}