using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanDisbursementCharges;

public partial class LoanDisbursementChargeChargeConfiguration : BaseEntityConfiguration<LoanDisbursementCharge, string>
{
    public override void Configure(EntityTypeBuilder<LoanDisbursementCharge> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanDisbursementCharge), DbSchemaConstants.Loans);

        entity.Property(lpc => lpc.LoanDisbursementId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.LoanDisbursement)
            .WithMany(e => e.LoanDisbursementCharges)
            .HasForeignKey(e => e.LoanDisbursementId)
            .IsRequired();

        entity.Property(lpc => lpc.DisbursementChargeId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.DisbursementCharge)
            .WithMany()
            .HasForeignKey(e => e.DisbursementChargeId)
            .IsRequired();

        entity.Property(e => e.TotalCharge)
            .HasPrecision(18, 2)
            .HasDefaultValue(0.00)
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

    partial void OnConfigurePartial(EntityTypeBuilder<LoanDisbursementCharge> entity);
}