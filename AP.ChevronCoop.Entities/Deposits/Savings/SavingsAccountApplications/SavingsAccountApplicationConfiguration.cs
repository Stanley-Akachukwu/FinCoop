using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;

public partial class SavingsAccountApplicationConfiguration : BaseEntityConfiguration<SavingsAccountApplication, string>
{
    public override void Configure(EntityTypeBuilder<SavingsAccountApplication> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(SavingsAccountApplication), DbSchemaConstants.Deposits);

        entity.HasIndex(x => new { x.ApplicationNo }).IsUnique();
        entity.HasIndex(x => new { x.DepositProductId });
        entity.HasIndex(x => new { x.CustomerId });


        entity.Property(e => e.ApplicationNo)
          .HasColumnType("nvarchar")
          .HasMaxLength(100)
          .IsRequired();

        entity.HasOne(e => e.DepositProduct)
          .WithMany()
          .HasForeignKey(e => e.DepositProductId)
          .IsRequired();

        entity.HasOne(e => e.Customer)
          .WithMany()
          .HasForeignKey(e => e.CustomerId)
          .IsRequired();

        entity.HasOne(e => e.Approval)
            .WithMany()
            .HasForeignKey(e => e.ApprovalId)
            .IsRequired(false);


        entity.Property(e => e.Amount).HasPrecision(18, 2)
               .HasDefaultValue(0.00)
               .IsRequired();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<SavingsAccountApplication> entity);
}