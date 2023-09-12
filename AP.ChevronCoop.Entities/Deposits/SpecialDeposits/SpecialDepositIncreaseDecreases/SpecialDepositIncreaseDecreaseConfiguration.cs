using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;

public partial class SpecialDepositIncreaseDecreaseConfiguration : BaseEntityConfiguration<SpecialDepositIncreaseDecrease, string>
{
    public override void Configure(EntityTypeBuilder<SpecialDepositIncreaseDecrease> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(SpecialDepositIncreaseDecrease), DbSchemaConstants.Deposits);


        entity.Property(e => e.Amount)
          .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

        entity.HasOne(e => e.SpecialDepositAccount)
           .WithMany()
           .HasForeignKey(e => e.SpecialDepositAccountId);

        entity.HasOne(e => e.Approval)
         .WithMany()
         .HasForeignKey(e => e.ApprovalId)
         .IsRequired(false);


        entity.Property(e => e.ContributionChangeRequest).HasMaxLength(100).HasConversion<string>();


        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<SpecialDepositIncreaseDecrease> entity);
}