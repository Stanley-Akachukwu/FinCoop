using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges
{
    public partial class DepositProductChargeConfiguration : BaseEntityConfiguration<DepositProductCharge, string>
    {
        public override void Configure(EntityTypeBuilder<DepositProductCharge> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(DepositProductCharge), DbSchemaConstants.Deposits);

            entity.HasIndex(x => new { x.ProductId, x.ChargeId }).IsUnique();

            entity.Property(lpc => lpc.ProductId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired();


            entity.HasOne(c => c.Product)
           .WithMany(p => p.Charges)
           .HasForeignKey(c => c.ProductId);

            //entity.HasOne(lpc => lpc.Product)
            //  .WithMany()
            //  .HasForeignKey(lpc => lpc.ProductId);

            entity.Property(lpc => lpc.ChargeId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired();

            entity.HasOne(lpc => lpc.Charge)
              .WithMany()
              .HasForeignKey(lpc => lpc.ChargeId);

            entity.Property(e => e.ChargeType).HasMaxLength(128).HasConversion<string>();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DepositProductCharge> entity);
    }
}
