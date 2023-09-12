using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.LoanOffsetTransactions;

public partial class LoanOffSetChargeConfiguration : BaseEntityConfiguration<LoanOffSetCharge, string>
{
    public override void Configure(EntityTypeBuilder<LoanOffSetCharge> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanOffSetCharge), DbSchemaConstants.Loans);

        entity.Property(oc => oc.TotalCharge)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lpc => lpc.LoanOffsetId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(oc => oc.LoanOffset)
            .WithMany(e => e.LoanOffSetCharges)
            .HasForeignKey(oc => oc.LoanOffsetId)
            .IsRequired();

        entity.Property(lpc => lpc.OffsetChargeId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(oc => oc.OffsetCharge)
          .WithMany()
          .HasForeignKey(oc => oc.OffsetChargeId)
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

    partial void OnConfigurePartial(EntityTypeBuilder<LoanOffSetCharge> entity);
}