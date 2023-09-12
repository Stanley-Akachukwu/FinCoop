using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using System.Reflection.Emit;

namespace AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges
{
    public partial class DepositProductInterestRangeConfiguration : BaseEntityConfiguration<DepositProductInterestRange, string>
    {
        public override void Configure(EntityTypeBuilder<DepositProductInterestRange> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(DepositProductInterestRange), DbSchemaConstants.Deposits);

            entity.HasIndex(x => new { x.ProductId, x.LowerLimit, x.UpperLimit }).IsUnique();

            entity.Property(lpc => lpc.ProductId)
              .HasColumnType("nvarchar")
            .HasMaxLength(40)
              .IsRequired();



            entity.HasOne(c => c.Product)
            .WithMany(p => p.InterestRanges)
            .HasForeignKey(c => c.ProductId);

            //entity.HasOne(lpc => lpc.Product)
            //  .WithMany()
            //  .HasForeignKey(lpc => lpc.ProductId);

            entity.Property(lpc => lpc.UpperLimit)
              .HasPrecision(30, 6)
              .IsRequired();

            entity.Property(lpc => lpc.LowerLimit)
               .HasPrecision(30, 6)
               .IsRequired();

            entity.Property(lpc => lpc.InterestRate)
             .HasPrecision(30, 6)
             .IsRequired();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DepositProductInterestRange> entity);
    }
}
