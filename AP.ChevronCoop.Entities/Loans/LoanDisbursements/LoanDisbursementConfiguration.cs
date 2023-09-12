using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanDisbursements;

public partial class LoanDisbursementConfiguration : BaseEntityConfiguration<LoanDisbursement, string>
{
    public override void Configure(EntityTypeBuilder<LoanDisbursement> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanDisbursement), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.LoanAccountId });

        entity.Property(lpc => lpc.LoanAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.LoanAccount)
          .WithMany(e => e.Disbursements)
          .HasForeignKey(e => e.LoanAccountId)
          .IsRequired();

        entity.Property(e => e.Amount)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(t => t.DisbursementDate)
          .IsRequired(false);

        entity.Property(t => t.ApprovalDate)
          .IsRequired(false);

        entity.Property(lpc => lpc.ApprovedByUserId)
          .HasColumnType("nvarchar")
          .HasMaxLength(450);

        entity.HasOne(t => t.ApprovedByUser)
          .WithMany()
          .HasForeignKey(t => t.ApprovedByUserId)
          .IsRequired(false);

        entity.Property(lpc => lpc.DisbursedByUserId)
          .HasColumnType("nvarchar")
          .HasMaxLength(450);

        entity.HasOne(t => t.DisbursedByUser)
          .WithMany()
          .HasForeignKey(t => t.DisbursedByUserId)
          .IsRequired(false);

        entity.Property(lpc => lpc.DisbursementAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(t => t.DisbursementAccount)
          .WithMany()
          .HasForeignKey(t => t.DisbursementAccountId)
          .IsRequired(false);

        entity.Property(lpc => lpc.CustomerBankAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(t => t.CustomerBankAccount)
          .WithMany()
          .HasForeignKey(t => t.CustomerBankAccountId)
          .IsRequired(false);

        entity.Property(lpc => lpc.TransactionJournalId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(t => t.TransactionJournal)
          .WithMany()
          .HasForeignKey(t => t.TransactionJournalId)
          .IsRequired(false);

        entity.Property(lpc => lpc.ApprovalId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.Property(lpc => lpc.SpecialDepositAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.SpecialDepositAccount)
          .WithMany()
          .HasForeignKey(e => e.SpecialDepositAccountId)
          .IsRequired(false);

        entity.HasOne(e => e.Approval)
          .WithMany()
          .HasForeignKey(e => e.ApprovalId)
          .IsRequired(false);

        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.DisbursementStatus).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.DisbursementMode).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.TransactionType).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanDisbursement> entity);
}