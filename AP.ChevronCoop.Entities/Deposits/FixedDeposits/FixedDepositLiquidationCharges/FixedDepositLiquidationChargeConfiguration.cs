using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositLiquidationCharges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;

public partial class FixedDepositLiquidationChargeConfiguration : BaseEntityConfiguration<FixedDepositLiquidationCharge, string>
{
    public override void Configure(EntityTypeBuilder<FixedDepositLiquidationCharge> entity)
    {
        base.Configure(entity);
        entity.ToTable(nameof(FixedDepositLiquidationCharge), DbSchemaConstants.Deposits);




        entity.Property(e => e.FixedDepositLiquidationId);
        entity.HasOne(e => e.FixedDepositLiquidation)
          .WithMany(e => e.LiquidationCharges)
          .HasForeignKey(e => e.FixedDepositLiquidationId);



        entity.Property(e => e.TransactionJournalId);
        entity.HasOne(e => e.TransactionJournal)
       .WithMany()
       .HasForeignKey(e => e.TransactionJournalId);


       entity.Property(e => e.ChargeType).IsRequired()
            .HasDefaultValue(ChargeType.FIXED_DEPOSIT_LIQUIDATION_CHARGE)
            .HasMaxLength(100)
            .HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<FixedDepositLiquidationCharge> entity);
}




 

 