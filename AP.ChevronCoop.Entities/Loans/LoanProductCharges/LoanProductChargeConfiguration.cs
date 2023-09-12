using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanProductCharges;

public partial class LoanProductChargeConfiguration : BaseEntityConfiguration<LoanProductCharge, string>
{
    public override void Configure(EntityTypeBuilder<LoanProductCharge> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanProductCharge), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.ProductId });

        entity.Property(lpc => lpc.ProductId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40)
            .IsRequired();

        entity.HasOne(lpc => lpc.Product)
            .WithMany()
            .HasForeignKey(lpc => lpc.ProductId)
            .IsRequired();

        entity.Property(lpc => lpc.ChargeId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(lpc => lpc.Charge)
          .WithMany()
          .HasForeignKey(lpc => lpc.ChargeId)
          .IsRequired();

        entity.Property(e => e.ChargeType).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanProductCharge> entity);
}