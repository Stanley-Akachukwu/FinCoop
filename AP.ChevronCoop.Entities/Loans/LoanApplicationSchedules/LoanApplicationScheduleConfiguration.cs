using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanApplicationSchedules;

public partial class LoanApplicationScheduleConfiguration : BaseEntityConfiguration<LoanApplicationSchedule, string>
{
    public override void Configure(EntityTypeBuilder<LoanApplicationSchedule> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanApplicationSchedule), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.LoanApplicationId });
        entity.HasIndex(x => new { x.LoanApplicationId, x.RepaymentNo }).IsUnique();

        entity.Property(lpc => lpc.LoanApplicationId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.LoanApplication)
           .WithMany()
           .HasForeignKey(e => e.LoanApplicationId)
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

    partial void OnConfigurePartial(EntityTypeBuilder<LoanApplicationSchedule> entity);
}