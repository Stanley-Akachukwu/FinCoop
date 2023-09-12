using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanRepayment;

public partial class LoanRepaymentChargeConfiguration : BaseEntityConfiguration<LoanRepaymentCharge, string>
{
    public override void Configure(EntityTypeBuilder<LoanRepaymentCharge> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanRepaymentCharge), DbSchemaConstants.Loans);

        entity.Property(lrc => lrc.TotalCharge)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lpc => lpc.LoanRepaymentId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(lrc => lrc.LoanRepayment)
            .WithMany()
            .HasForeignKey(lrc => lrc.LoanRepaymentId)
            .IsRequired();

        entity.Property(lpc => lpc.RepaymentChargeId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(lrc => lrc.RepaymentCharge)
            .WithMany()
            .HasForeignKey(lrc => lrc.RepaymentChargeId)
            .IsRequired();
        
        entity.Property(lpc => lpc.TransactionJournalId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(t => t.TransactionJournal)
            .WithMany()
            .HasForeignKey(t => t.TransactionJournalId)
            .IsRequired(false);

        entity.Property(e => e.ChargeType).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanRepaymentCharge> entity);
}