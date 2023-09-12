using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.CoopCustomers.CustomerBeneficiaries;

public partial class CustomerBeneficiaryConfiguration : BaseEntityConfiguration<CustomerBeneficiary, string>
{
    public override void Configure(EntityTypeBuilder<CustomerBeneficiary> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(CustomerBeneficiary), DbSchemaConstants.Customer);

        entity.Property(e => e.LastName)
          .HasColumnType("nvarchar")
          .HasMaxLength(128)
          .IsRequired();

        entity.Property(e => e.FirstName)
          .HasColumnType("nvarchar")
          .HasMaxLength(128)
          .IsRequired();

        entity.Property(e => e.Address)
          .HasColumnType("nvarchar")
          .HasMaxLength(512)
          .IsRequired();

        entity.Property(e => e.Phone)
          .HasColumnType("nvarchar")
          .HasMaxLength(32)
          .IsRequired();

        entity.Property(e => e.Email)
          .HasColumnType("nvarchar")
          .HasMaxLength(128);

        entity.HasOne(e => e.Customer)
           .WithMany()
           .HasForeignKey(e => e.CustomerId)
           .IsRequired();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<CustomerBeneficiary> entity);
}