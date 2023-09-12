using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositChangeInMaturities;

public partial class
  FixedDepositChangeInMaturityConfiguration : BaseEntityConfiguration<FixedDepositChangeInMaturity, string>
{
    public override void Configure(EntityTypeBuilder<FixedDepositChangeInMaturity> entity)
    {
        base.Configure(entity);
        entity.ToTable(nameof(FixedDepositChangeInMaturity), DbSchemaConstants.Deposits);

        entity.HasIndex(x => new { x.FixedDepositAccountId });

        entity.Property(e => e.FixedDepositAccountId);
        entity.HasOne(e => e.FixedDepositAccount)
          .WithMany()
          .HasForeignKey(e => e.FixedDepositAccountId)
          .IsRequired();


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

        entity.Property(e => e.MaturityInstructionType).HasMaxLength(100).IsRequired().HasConversion<string>();
        entity.Property(e => e.LiquidationAccountType).HasMaxLength(100).IsRequired().HasConversion<string>();

        entity.HasOne(e => e.Approval)
            .WithMany()
            .HasForeignKey(e => e.ApprovalId)
            .IsRequired(false);


        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<FixedDepositChangeInMaturity> entity);
}