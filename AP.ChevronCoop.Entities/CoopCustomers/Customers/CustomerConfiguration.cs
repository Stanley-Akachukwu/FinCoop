using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.CoopCustomers.Customers;

public partial class CustomerConfiguration : BaseEntityConfiguration<Customer, string>
{
    public override void Configure(EntityTypeBuilder<Customer> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(Customer), DbSchemaConstants.Customer);

        entity.HasIndex(x => new { x.ApplicationUserId }).IsUnique();
        //entity.HasIndex(x => new { x.AdObjectId, x.PrimaryEmail }).IsUnique();

        //entity.Property(e => e.AdObjectId)
        //  .HasColumnName("AdObjectId")
        //  .HasColumnType("nvarchar")
        //  .HasMaxLength(50)
        //  .IsRequired();

        entity.Property(e => e.DepartmentId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40)
      .IsRequired(false);


        entity.Property(e => e.CashAccountId).HasColumnType("nvarchar").HasMaxLength(40);
        entity.HasOne(e => e.CashAccount)
          .WithMany()
          .HasForeignKey(e => e.CashAccountId)
          .IsRequired();


        // entity.Property(e => e.ProfileImageUrl)
        //   .HasColumnType("nvarchar")
        //   .HasMaxLength(1024);

        // entity.Property(e => e.PassportUrl)
        //   .HasColumnType("nvarchar")
        //   .HasMaxLength(1024);

        // entity.Property(e => e.PrimaryEmail)
        //   .HasColumnType("nvarchar")
        //   .HasMaxLength(128)
        //   .IsRequired();
        //
        // entity.Property(e => e.PrimaryPhone)
        //   .HasColumnType("nvarchar")
        //   .HasMaxLength(64);
        //
        // entity.Property(e => e.SecondaryEmail)
        //   .HasColumnType("nvarchar")
        //   .HasMaxLength(128)
        //   .IsRequired();
        //
        // entity.Property(e => e.SecondaryPhone)
        //   .HasColumnType("nvarchar")
        //   .HasMaxLength(64);

        entity.Property(e => e.LastName)
          .HasColumnName("LastName")
          .HasColumnType("nvarchar")
          .HasMaxLength(128);

        entity.Property(e => e.MiddleName)
          .HasColumnName("MiddleName")
          .HasColumnType("nvarchar")
          .HasMaxLength(128);

        entity.Property(e => e.FirstName)
          .HasColumnName("FirstName")
          .HasColumnType("nvarchar")
          .HasMaxLength(128);

        //entity.Property(e => e.ApprovalId)
        //  .HasColumnType("nvarchar")
        //  .HasMaxLength(40);

        entity.Property(e => e.Status).HasMaxLength(32).HasConversion<string>();
        entity.Property(e => e.Gender).HasMaxLength(32).HasConversion<string>();
        entity.Property(e => e.MemberType).HasMaxLength(32).HasConversion<string>();
        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Customer> entity);
}