using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.Products.CustomerDepositProductPublications
{
    public partial class CustomerDepositProductPublicationConfiguration : BaseEntityConfiguration<CustomerDepositProductPublication, string>
    {
        public override void Configure(EntityTypeBuilder<CustomerDepositProductPublication> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(CustomerDepositProductPublication), DbSchemaConstants.Deposits);

            entity.HasIndex(x => new { x.ProductId });

            entity.Property(lpc => lpc.ProductId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired();

            entity.HasOne(lpc => lpc.Product)
              .WithMany()
              .HasForeignKey(lpc => lpc.ProductId);

            entity.Property(e => e.PublicationType).HasMaxLength(100).HasConversion<string>();

            //entity.Property(lpc => lpc.CustomerId)
            //  .HasColumnType("nvarchar")
            //  .HasMaxLength(40)
            //  .IsRequired();

            //entity.HasOne(lpc => lpc.Customer)
            //  .WithMany()
            //  .HasForeignKey(lpc => lpc.CustomerId);


            OnConfigurePartial(entity);
        }
        partial void OnConfigurePartial(EntityTypeBuilder<CustomerDepositProductPublication> entity);
    }
}
