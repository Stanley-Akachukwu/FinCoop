using AP.ChevronCoop.Entities.Loans.LoanProductPublications.MemberLoanProductPublications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.CustomerLoanProductPublications;

public partial class
  CustomerLoanProductPublicationConfiguration : BaseEntityConfiguration<CustomerLoanProductPublication, string>
{
  public override void Configure(EntityTypeBuilder<CustomerLoanProductPublication> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(CustomerLoanProductPublication), DbSchemaConstants.Loans);

    entity.Property(lpc => lpc.ProductId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40);

    entity.HasOne(lpc => lpc.Product)
      .WithMany()
      .HasForeignKey(lpc => lpc.ProductId)
      .IsRequired();

    entity.Property(lpc => lpc.CustomerId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40);

    entity.HasOne(lpc => lpc.Customer)
      .WithMany()
      .HasForeignKey(lpc => lpc.CustomerId)
      .IsRequired();

    entity.Property(e => e.PublicationType).HasMaxLength(100).HasConversion<string>();

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<CustomerLoanProductPublication> entity);
}