using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanRepayment;

public partial class LoanRepaymentConfiguration : BaseEntityConfiguration<LoanRepayment, string>
{
    public override void Configure(EntityTypeBuilder<LoanRepayment> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanRepayment), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.LoanAccountId });

        entity.Property(lpc => lpc.LoanAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(t => t.LoanAccount)
          .WithMany()
          .HasForeignKey(t => t.LoanAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.PayrollDeductionScheduleItemId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(t => t.PayrollDeductionScheduleItem)
          .WithMany()
          .HasForeignKey(t => t.PayrollDeductionScheduleItemId)
          .IsRequired(false);

        entity.Property(lpc => lpc.LoanRepaymentScheduleId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(t => t.LoanRepaymentSchedule)
          .WithMany()
          .HasForeignKey(t => t.LoanRepaymentScheduleId)
          .IsRequired(false);

        entity.Property(t => t.Amount)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(t => t.RepaymentDate)
          .IsRequired(false);

        entity.Property(lpc => lpc.ApprovalId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.Approval)
          .WithMany()
          .HasForeignKey(e => e.ApprovalId)
          .IsRequired(false);

        entity.Property(lpc => lpc.PaymentAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(t => t.PaymentAccount)
          .WithMany()
          .HasForeignKey(t => t.PaymentAccountId)
          .IsRequired(false);

        entity.Property(lpc => lpc.LoanOffsetId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(t => t.LoanOffset)
          .WithMany()
          .HasForeignKey(t => t.LoanOffsetId)
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

        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.RepaymentMode).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanRepayment> entity);
}