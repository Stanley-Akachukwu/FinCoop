using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;

public partial class SavingsIncreaseDecreaseConfiguration : BaseEntityConfiguration<SavingsIncreaseDecrease, string>
{
    public override void Configure(EntityTypeBuilder<SavingsIncreaseDecrease> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(SavingsIncreaseDecrease), DbSchemaConstants.Deposits);


        entity.Property(e => e.Amount)
          .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

        entity.HasOne(e => e.SavingsAccount)
           .WithMany()
           .HasForeignKey(e => e.SavingsAccountId);

        entity.HasOne(e => e.Approval)
         .WithMany()
         .HasForeignKey(e => e.ApprovalId)
         .IsRequired(false);


        entity.Property(e => e.ContributionChangeRequest).HasMaxLength(100).HasConversion<string>();


        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<SavingsIncreaseDecrease> entity);
}