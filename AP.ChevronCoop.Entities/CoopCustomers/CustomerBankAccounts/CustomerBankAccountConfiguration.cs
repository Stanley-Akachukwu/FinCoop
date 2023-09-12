using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;

public partial class CustomerBankAccountConfiguration : BaseEntityConfiguration<CustomerBankAccount, string>
{
    public override void Configure(EntityTypeBuilder<CustomerBankAccount> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(CustomerBankAccount), DbSchemaConstants.Customer);

        entity.Property(e => e.AccountNumber)
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();

        entity.Property(e => e.AccountName)
          .HasColumnType("nvarchar")
          .HasMaxLength(128)
          .IsRequired();

        entity.Property(e => e.BVN)
          .HasColumnType("nvarchar")
          .HasMaxLength(32)
          .IsRequired(false);

        entity.Property(e => e.Branch)
          .HasColumnType("nvarchar")
          .HasMaxLength(64);
        
        entity.Property(e => e.CustomerId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40)
          .IsRequired();

        entity.HasOne(e => e.Customer)
          .WithMany()
          .HasForeignKey(e => e.CustomerId);
        
        
        entity.Property(e => e.BankId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40)
          .IsRequired();

        entity.HasOne(e => e.Bank)
          .WithMany()
          .HasForeignKey(e => e.BankId);
        
        
        entity.Property(e => e.LedgerAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40)
          .IsRequired();

        entity.HasOne(e => e.LedgerAccount)
          .WithMany()
          .HasForeignKey(e => e.LedgerAccountId);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<CustomerBankAccount> entity);
}