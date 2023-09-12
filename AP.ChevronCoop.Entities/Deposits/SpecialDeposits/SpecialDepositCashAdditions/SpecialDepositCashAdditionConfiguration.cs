using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions
{
    public partial class SpecialDepositCashAdditionConfiguration : BaseEntityConfiguration<SpecialDepositCashAddition, string>
    {
        public override void Configure(EntityTypeBuilder<SpecialDepositCashAddition> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(SpecialDepositCashAddition), DbSchemaConstants.Deposits);

            entity.HasIndex(x => new { x.BatchRefNo }).IsUnique();
            entity.Property(e => e.BatchRefNo).HasColumnType("nvarchar").HasMaxLength(40);


            entity.Property(s => s.Amount)
                .IsRequired()
                .HasPrecision(18, 2); // Assuming Amount is a decimal number with 2 decimal places


            entity.HasOne(e => e.TransactionJournal)
           .WithMany()
           .HasForeignKey(e => e.TransactionJournalId);

            entity.HasOne(s => s.SpecialDepositAccount)
                .WithMany()
                .HasForeignKey(s => s.SpecialDepositAccountId);

            entity.HasOne(s => s.CustomerPaymentDocument)
                .WithMany()
                .HasForeignKey(s => s.CustomerPaymentDocumentId);

            entity.Property(e => e.ApprovalId).HasColumnType("nvarchar").HasMaxLength(40);
            entity.HasOne(e => e.Approval)
                .WithMany()
                .HasForeignKey(e => e.ApprovalId);

            entity.Property(e => e.ModeOfPayment).HasMaxLength(100).HasConversion<string>();
            entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SpecialDepositCashAddition> entity);
    }

}