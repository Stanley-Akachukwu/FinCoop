using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace AP.ChevronCoop.Entities.LoanOffsetTransactions;

public partial class LoanOffsetConfiguration : BaseEntityConfiguration<LoanOffset, string>
{
    public override void Configure(EntityTypeBuilder<LoanOffset> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanOffset), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.LoanAccountId });

        entity.Property(lpc => lpc.LoanAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(lo => lo.LoanAccount)
          .WithMany()
          .HasForeignKey(lo => lo.LoanAccountId)
          .IsRequired();

        entity.Property(lo => lo.OffsetAmount)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lo => lo.OldPrincipalBalance)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lo => lo.NewPrincipalBalance)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lo => lo.OldInterestBalance)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lo => lo.NewInterestBalance)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lo => lo.TotalOffsetCharges)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lo => lo.IsLiquidated).IsRequired();

        entity.Property(lpc => lpc.SavingsAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(lo => lo.SavingsAccount)
          .WithMany()
          .HasForeignKey(lo => lo.SavingsAccountId)
          .IsRequired(false);

        entity.Property(lpc => lpc.SpecialDepositAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(lo => lo.SpecialDepositAccount)
          .WithMany()
          .HasForeignKey(lo => lo.SpecialDepositAccountId)
          .IsRequired(false);

        entity.Property(lpc => lpc.CustomerBankAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(lo => lo.CustomerBankAccount)
          .WithMany()
          .HasForeignKey(lo => lo.CustomerBankAccountId)
          .IsRequired(false);

        entity.Property(lo => lo.ModeOfPayment).IsRequired();

        entity.Property(lpc => lpc.CustomerPaymentDocumentId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(lo => lo.CustomerPaymentDocument)
          .WithMany()
          .HasForeignKey(lo => lo.CustomerPaymentDocumentId)
          .IsRequired(false);

        entity.Property(lpc => lpc.TransactionJournalId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(lo => lo.TransactionJournal)
          .WithMany()
          .HasForeignKey(lo => lo.TransactionJournalId)
          .IsRequired(false);

        entity.Property(lpc => lpc.ApprovalId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.Approval)
          .WithMany()
          .HasForeignKey(e => e.ApprovalId)
          .IsRequired(false);

        entity.Property(e => e.AllowedOffsetType).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.LoanRepaymentMode).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();

        entity.Property(e => e.RepaymentSchedules)
          .HasConversion(
            v => JsonSerializer.Serialize(v, typeof(List<string>), JsonSerializerOptions.Default),
            v => JsonSerializer.Deserialize<List<string>>(v, JsonSerializerOptions.Default)
          );

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanOffset> entity);
}