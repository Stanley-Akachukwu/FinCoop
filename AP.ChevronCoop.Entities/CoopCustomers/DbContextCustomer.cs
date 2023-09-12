using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBeneficiaries;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerNextOfKins;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.Entities;

public partial class ChevronCoopDbContext //: DbContext
{
  public DbSet<Customer> Customers { get; set; }
  public DbSet<CustomerMasterView> CustomerMasterView { get; set; }

  public DbSet<CustomerBankAccount> CustomerBankAccounts { get; set; }
  public DbSet<CustomerBankAccountMasterView> CustomerBankAccountMasterView { get; set; }

  public DbSet<CustomerBeneficiary> CustomerBeneficiaries { get; set; }
  public DbSet<CustomerBeneficiaryMasterView> CustomerBeneficiaryMasterView { get; set; }

  public DbSet<CustomerNextOfKin> CustomerNextOfKins { get; set; }
  public DbSet<CustomerNextOfKinMasterView> CustomerNextOfKinMasterView { get; set; }


  public void OnModelCreating_Customers(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    modelBuilder.ApplyConfiguration(new CustomerBankAccountConfiguration());
    modelBuilder.ApplyConfiguration(new CustomerBeneficiaryConfiguration());
    modelBuilder.ApplyConfiguration(new CustomerNextOfKinConfiguration());

    modelBuilder.Entity<CustomerMasterView>(b =>
    {
      b.ToTable(nameof(CustomerMasterView), DbSchemaConstants.Customer, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<CustomerBankAccountMasterView>(b =>
    {
      b.ToTable(nameof(CustomerBankAccountMasterView), DbSchemaConstants.Customer, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<CustomerBeneficiaryMasterView>(b =>
    {
      b.ToTable(nameof(CustomerBeneficiaryMasterView), DbSchemaConstants.Customer, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<CustomerNextOfKinMasterView>(b =>
    {
      b.ToTable(nameof(CustomerNextOfKinMasterView), DbSchemaConstants.Customer, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });
  }
}