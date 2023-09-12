using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;

public partial class FixedDepositLiquidationConfiguration : BaseEntityConfiguration<FixedDepositLiquidation, string>
{
    public override void Configure(EntityTypeBuilder<FixedDepositLiquidation> entity)
    {
        base.Configure(entity);
        entity.ToTable(nameof(FixedDepositLiquidation), DbSchemaConstants.Deposits);

  

        entity.Property(e => e.FixedDepositAccountId);
        entity.HasOne(e => e.FixedDepositAccount)
          .WithMany(e => e.FixedDepositLiquidations)
          .HasForeignKey(e => e.FixedDepositAccountId)
          .IsRequired();

        entity.Property(e => e.TransactionJournalId).HasMaxLength(40);
        entity.HasOne(e => e.TransactionJournal)
       .WithMany()
       .HasForeignKey(e => e.TransactionJournalId);

        entity.Property(e => e.SavingsLiquidationAccountId);
        entity.HasOne(e => e.SavingsLiquidationAccount)
          .WithMany()
          .HasForeignKey(e => e.SavingsLiquidationAccountId);


        entity.Property(e => e.CustomerBankLiquidationAccountId);
        entity.HasOne(e => e.CustomerBankLiquidationAccount)
          .WithMany()
          .HasForeignKey(e => e.CustomerBankLiquidationAccountId);


        entity.Property(e => e.SpecialDepositLiquidationAccountId);
        entity.HasOne(e => e.SpecialDepositLiquidationAccount)
          .WithMany()
          .HasForeignKey(e => e.SpecialDepositLiquidationAccountId);


        entity.HasOne(e => e.Approval)
            .WithMany()
            .HasForeignKey(e => e.ApprovalId)
            .IsRequired(false);

        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.LiquidationAccountType).HasMaxLength(100).HasConversion<string>();

        // entity.Property(e => e.MaturityInstructionType).IsRequired().HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<FixedDepositLiquidation> entity);
}




 

 