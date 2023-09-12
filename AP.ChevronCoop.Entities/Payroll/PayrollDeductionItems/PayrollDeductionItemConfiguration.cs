using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Payroll;

public partial class PayrollDeductionItemConfiguration : BaseEntityConfiguration<PayrollDeductionItem, string>
{
    public override void Configure(EntityTypeBuilder<PayrollDeductionItem> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(PayrollDeductionItem), DbSchemaConstants.Payroll);

        entity.HasIndex(x => new { x.PayrollDeductionScheduleId });

        entity.Property(p => p.MemberId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40)
          .IsRequired();

        entity.Property(p => p.MemberName)
          .HasColumnType("nvarchar")
          .HasMaxLength(120)
          .IsRequired();

        entity.Property(p => p.AccountNo)
          .IsRequired(false)
          .HasMaxLength(32);

        entity.Property(p => p.Amount)
          .HasPrecision(18, 2)
          .IsRequired(); // Adjust the precision and scale as per your requirements

        entity.Property(p => p.PayrollCode)
          .IsRequired()
          .HasMaxLength(255);

        entity.Property(p => p.Narration)
          .IsRequired(false)
          .HasMaxLength(255);

        entity.Property(p => p.PayrollDate)
          .IsRequired();

        entity.Property(p => p.TotalDeduction)
          .HasPrecision(18, 2)
          .IsRequired(false);
        // Adjust the precision and scale as per your requirementsbuilder.Property(pd => pd.PayrollDeductionScheduleId).IsRequired();

        // entity.Property(e => e.PayrollErrors)
        //   .HasConversion(
        //     v => JsonSerializer.Serialize(v, typeof(List<String>), JsonSerializerOptions.Default),
        //     v => JsonSerializer.Deserialize<List<String>>(v, JsonSerializerOptions.Default)
        //   );

        // Configure the relationship with PayrollDeductionSchedule entity


        entity.Property(pd => pd.PayrollDeductionScheduleId).HasMaxLength(40).IsRequired();
        entity.HasOne(pd => pd.PayrollDeductionSchedule)
          .WithMany(pd => pd.DeductionItems)
          .HasForeignKey(pd => pd.PayrollDeductionScheduleId)
          .IsRequired();


        entity.Property(e => e.DeductionType).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.CurrentStatus).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<PayrollDeductionItem> entity);
}