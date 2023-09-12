using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.Loans.CustomerLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.DepartmentLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplicationApprovals;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities.Loans.LoanApplicationItems;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.Entities.Loans.LoanDisbursementCharges;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities.Loans.LoanOffSetCharges;
using AP.ChevronCoop.Entities.Loans.LoanOffSetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanProductCharges;
using AP.ChevronCoop.Entities.Loans.LoanProductPublications.DepartmentLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.LoanProductPublications.MemberLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.Loans.LoanRepayment;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentCharges;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Entities.Loans.LoanTopupCharges;
using AP.ChevronCoop.Entities.Loans.LoanTopupTransactions;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.Entities;

public partial class ChevronCoopDbContext //: DbContext
{
  public DbSet<CustomerLoanProductPublication> CustomerLoanProductPublications { get; set; }
  public DbSet<DepartmentLoanProductPublication> DepartmentLoanProductPublications { get; set; }
  public DbSet<LoanAccount> LoanAccounts { get; set; }
  public DbSet<LoanApplicationApproval> LoanApplicationApprovals { get; set; }
  public DbSet<LoanApplicationGuarantor> LoanApplicationGuarantors { get; set; }
  public DbSet<LoanApplicationItem> LoanApplicationItems { get; set; }
  public DbSet<LoanApplication> LoanApplications { get; set; }
  public DbSet<LoanApplicationSchedule> LoanApplicationSchedules { get; set; }

  public DbSet<LoanDisbursementCharge> LoanDisbursementCharges { get; set; }

  public DbSet<LoanDisbursement> LoanDisbursements { get; set; }

  public DbSet<LoanOffSetCharge> LoanOffSetCharges { get; set; }

  public DbSet<LoanOffset> LoanOffsets { get; set; }

  public DbSet<LoanProductCharge> LoanProductCharges { get; set; }

  // public DbSet<LoanProductPublication> LoanProductPublications { get; set; }
  public DbSet<LoanProduct> LoanProducts { get; set; }
  public DbSet<LoanRepayment> LoanRepayments { get; set; }
  public DbSet<LoanRepaymentCharge> LoanRepaymentCharges { get; set; }
  public DbSet<LoanRepaymentSchedule> LoanRepaymentSchedules { get; set; }
  public DbSet<LoanTopup> LoanTopups { get; set; }
  public DbSet<LoanTopupCharge> LoanTopupCharges { get; set; }


  public DbSet<LoanRepaymentScheduleMasterView> LoanRepaymentScheduleMasterView { get; set; }
  public DbSet<LoanApplicationScheduleMasterView> LoanApplicationScheduleMasterView { get; set; }
  public DbSet<LoanDisbursementMasterView> LoanDisbursementMasterView { get; set; }
  public DbSet<LoanProductChargeMasterView> LoanProductChargeMasterView { get; set; }
  public DbSet<LoanApplicationApprovalMasterView> LoanApplicationApprovalMasterView { get; set; }
  public DbSet<LoanAccountMasterView> LoanAccountMasterView { get; set; }
  public DbSet<LoanApplicationGuarantorMasterView> LoanApplicationGuarantorMasterView { get; set; }
  public DbSet<LoanApplicationGuarantorApprovalMasterView> LoanApplicationGuarantorApprovalMasterView { get; set; }
  public DbSet<LoanApplicationMasterView> LoanApplicationMasterView { get; set; }
  public DbSet<LoanApplicationItemMasterView> LoanApplicationItemMasterView { get; set; }
  public DbSet<LoanProductMasterView> LoanProductMasterView { get; set; }

  public DbSet<LoanTopupChargeMasterView> LoanTopupChargeMasterView { get; set; }
  public DbSet<CustomerLoanProductPublicationMasterView> CustomerLoanProductPublicationMasterView { get; set; }
  public DbSet<DepartmentLoanProductPublicationMasterView> DepartmentLoanProductPublicationMasterView { get; set; }
  public DbSet<LoanDisbursementChargeMasterView> LoanDisbursementChargeMasterView { get; set; }
  public DbSet<LoanOffsetMasterView> LoanOffsetMasterView { get; set; }
  public DbSet<LoanOffSetChargeMasterView> LoanOffSetChargeMasterView { get; set; }
  public DbSet<LoanRepaymentMasterView> LoanRepaymentMasterView { get; set; }
  public DbSet<LoanRepaymentChargeMasterView> LoanRepaymentChargeMasterView { get; set; }
  public DbSet<LoanTopupMasterView> LoanTopupMasterView { get; set; }


  public void OnModelCreating_Loans(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new LoanProductChargeConfiguration());
    modelBuilder.ApplyConfiguration(new LoanProductConfiguration());
    modelBuilder.ApplyConfiguration(new LoanApplicationConfiguration());
    modelBuilder.ApplyConfiguration(new LoanApplicationItemConfiguration());
    modelBuilder.ApplyConfiguration(new LoanApplicationGuarantorConfiguration());
    modelBuilder.ApplyConfiguration(new LoanRepaymentScheduleConfiguration());
    modelBuilder.ApplyConfiguration(new LoanApplicationScheduleConfiguration());
    modelBuilder.ApplyConfiguration(new CustomerLoanProductPublicationConfiguration());
    modelBuilder.ApplyConfiguration(new DepartmentLoanProductPublicationConfiguration());
    modelBuilder.ApplyConfiguration(new LoanAccountConfiguration());
    modelBuilder.ApplyConfiguration(new LoanDisbursementConfiguration());
    modelBuilder.ApplyConfiguration(new LoanApplicationApprovalConfiguration());
    modelBuilder.ApplyConfiguration(new LoanDisbursementChargeChargeConfiguration());
    modelBuilder.ApplyConfiguration(new LoanDisbursementConfiguration());
    modelBuilder.ApplyConfiguration(new LoanOffSetChargeConfiguration());
    modelBuilder.ApplyConfiguration(new LoanOffsetConfiguration());
    modelBuilder.ApplyConfiguration(new LoanRepaymentConfiguration());
    modelBuilder.ApplyConfiguration(new LoanRepaymentChargeConfiguration());
    modelBuilder.ApplyConfiguration(new LoanTopupConfiguration());
    modelBuilder.ApplyConfiguration(new LoanTopupChargeConfiguration());


    modelBuilder.Entity<LoanProductMasterView>(b =>
    {
      b.ToTable(nameof(LoanProductMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });


    modelBuilder.Entity<LoanProductChargeMasterView>(b =>
    {
      b.ToTable(nameof(LoanProductChargeMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });


    modelBuilder.Entity<LoanApplicationMasterView>(b =>
    {
      b.ToTable(nameof(LoanApplicationMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanApplicationItemMasterView>(b =>
    {
      b.ToTable(nameof(LoanApplicationItemMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanApplicationGuarantorMasterView>(b =>
    {
      b.ToTable(nameof(LoanApplicationGuarantorMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanRepaymentScheduleMasterView>(b =>
    {
      b.ToTable(nameof(LoanRepaymentScheduleMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanApplicationScheduleMasterView>(b =>
    {
      b.ToTable(nameof(LoanApplicationScheduleMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });


    modelBuilder.Entity<LoanAccountMasterView>(b =>
    {
      b.ToTable(nameof(LoanAccountMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanDisbursementMasterView>(b =>
    {
      b.ToTable(nameof(LoanDisbursementMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanApplicationApprovalMasterView>(b =>
    {
      b.ToTable(nameof(LoanApplicationApprovalMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanTopupChargeMasterView>(b =>
    {
      b.ToTable(nameof(LoanTopupChargeMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });


    modelBuilder.Entity<CustomerLoanProductPublicationMasterView>(b =>
    {
      b.ToTable(nameof(CustomerLoanProductPublicationMasterView), DbSchemaConstants.Loans,
        t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<DepartmentLoanProductPublicationMasterView>(b =>
    {
      b.ToTable(nameof(DepartmentLoanProductPublicationMasterView), DbSchemaConstants.Loans,
        t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanDisbursementChargeMasterView>(b =>
    {
      b.ToTable(nameof(LoanDisbursementChargeMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanOffsetMasterView>(b =>
    {
      b.ToTable(nameof(LoanOffsetMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanOffSetChargeMasterView>(b =>
    {
      b.ToTable(nameof(LoanOffSetChargeMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanRepaymentMasterView>(b =>
    {
      b.ToTable(nameof(LoanRepaymentMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanRepaymentChargeMasterView>(b =>
    {
      b.ToTable(nameof(LoanRepaymentChargeMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<LoanTopupMasterView>(b =>
    {
      b.ToTable(nameof(LoanTopupMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });
    
    modelBuilder.Entity<LoanApplicationGuarantorApprovalMasterView>(b =>
    {
      b.ToTable(nameof(LoanApplicationGuarantorApprovalMasterView), DbSchemaConstants.Loans, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });
  }
}