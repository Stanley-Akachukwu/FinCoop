using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.Products.DepartmentDepositProductPublications
{
    public partial class DepartmentDepositProductPublicationConfiguration : BaseEntityConfiguration<DepartmentDepositProductPublication, string>
    {
        public override void Configure(EntityTypeBuilder<DepartmentDepositProductPublication> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(DepartmentDepositProductPublication), DbSchemaConstants.Deposits);

            entity.HasIndex(x => new { x.ProductId });

            entity.Property(lpc => lpc.ProductId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired();

            entity.HasOne(lpc => lpc.Product)
              .WithMany()
              .HasForeignKey(lpc => lpc.ProductId);

            entity.Property(e => e.PublicationType).HasMaxLength(100).HasConversion<string>();

            entity.Property(lpc => lpc.DepartmentId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired();

            entity.HasOne(lpc => lpc.Department)
              .WithMany()
              .HasForeignKey(lpc => lpc.DepartmentId);
            OnConfigurePartial(entity);

        }

        partial void OnConfigurePartial(EntityTypeBuilder<DepartmentDepositProductPublication> entity);
    }
}
