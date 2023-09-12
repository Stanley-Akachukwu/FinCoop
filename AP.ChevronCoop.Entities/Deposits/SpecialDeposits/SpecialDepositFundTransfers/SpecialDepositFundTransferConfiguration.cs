using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer
{
    public partial class SpecialDepositFundTransferConfiguration : BaseEntityConfiguration<SpecialDepositFundTransfer, string>
    {
        public override void Configure(EntityTypeBuilder<SpecialDepositFundTransfer> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(SpecialDepositFundTransfer), DbSchemaConstants.Deposits);

            entity.HasKey(s => s.Id); // Primary key

            entity.Property(s => s.Amount)
                .IsRequired()
                .HasPrecision(18, 2); // Assuming Amount is a decimal number with 2 decimal places


            entity.HasOne(s => s.SpecialDepositAccount)
                .WithMany()
                .HasForeignKey(s => s.SpecialDepositAccountId);

            entity.HasOne(s => s.SavingsDestinationAccount)
                .WithMany()
                .HasForeignKey(s => s.SavingsDestinationAccountId);

            entity.HasOne(s => s.FixedDepositDestinationAccount)
                .WithMany()
                .HasForeignKey(s => s.FixedDepositDestinationAccountId);


            entity.HasOne(e => e.TransactionJournal)
           .WithMany()
           .HasForeignKey(e => e.TransactionJournalId);


            entity.Property(e => e.ApprovalId).HasColumnType("nvarchar").HasMaxLength(40);
            entity.HasOne(e => e.Approval)
                .WithMany()
                .HasForeignKey(e => e.ApprovalId);
            
            entity.Property(e => e.DestinationAccountType).HasMaxLength(100).HasConversion<string>();
            entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SpecialDepositFundTransfer> entity);
    }

}