using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;

public partial class LoanRepaymentScheduleConfiguration : BaseEntityConfiguration<LoanRepaymentSchedule, string>
{
    public override void Configure(EntityTypeBuilder<LoanRepaymentSchedule> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanRepaymentSchedule), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.LoanAccountId });

        entity.Property(lpc => lpc.LoanAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(t => t.LoanAccount)
          .WithMany()
          .HasForeignKey(t => t.LoanAccountId)
          .IsRequired();

        entity.Property(l => l.BatchRefNo)
          .IsRequired();

        entity.Property(l => l.DueDate)
          .IsRequired();

        entity.Property(e => e.PeriodPayment)
         .HasPrecision(18, 2)
         .HasDefaultValue(0.00)
         .IsRequired();

        entity.Property(e => e.CumulativeTotal)
         .HasPrecision(18, 2)
         .HasDefaultValue(0.00)
         .IsRequired();

        entity.Property(e => e.TotalBalance)
       .HasPrecision(18, 2)
         .HasDefaultValue(0.00)
         .IsRequired();

        entity.Property(e => e.PeriodPrincipal)
         .HasPrecision(18, 2)
         .HasDefaultValue(0.00)
         .IsRequired();

        entity.Property(e => e.CumulativePrincipal)
        .HasPrecision(18, 2)
        .HasDefaultValue(0.00)
        .IsRequired();

        entity.Property(e => e.PrincipalBalance)
        .HasPrecision(18, 2)
        .HasDefaultValue(0.00)
        .IsRequired();



        entity.Property(e => e.PeriodInterest)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(e => e.CumulativeInterest)
        .HasPrecision(18, 2)
        .HasDefaultValue(0.00)
        .IsRequired();

        entity.Property(e => e.InterestBalance)
        .HasPrecision(18, 2)
        .HasDefaultValue(0.00)
        .IsRequired();

        entity.Property(e => e.TenureUnit).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanRepaymentSchedule> entity);
}