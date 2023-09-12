using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.LoanTopupTransactions;

public partial class LoanTopupChargeConfiguration : BaseEntityConfiguration<LoanTopupCharge, string>
{
    public override void Configure(EntityTypeBuilder<LoanTopupCharge> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanTopupCharge), DbSchemaConstants.Loans);

        entity.Property(ltc => ltc.TotalCharge)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lpc => lpc.LoanTopupId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(ltc => ltc.LoanTopup)
            .WithMany(e => e.LoanTopupCharges)
            .HasForeignKey(ltc => ltc.LoanTopupId)
            .IsRequired();

        entity.Property(lpc => lpc.TopupChargeId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(ltc => ltc.TopupCharge)
          .WithMany()
          .HasForeignKey(ltc => ltc.TopupChargeId)
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

    partial void OnConfigurePartial(EntityTypeBuilder<LoanTopupCharge> entity);
}