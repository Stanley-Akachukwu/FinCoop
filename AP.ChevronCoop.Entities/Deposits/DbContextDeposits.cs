using AP.ChevronCoop.Entities.Deposits.CommonViews;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestSchedules;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositLiquidationCharges;
using AP.ChevronCoop.Entities.Deposits.Products.CustomerDepositProductPublications;
using AP.ChevronCoop.Entities.Deposits.Products.DepartmentDepositProductPublications;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountDeductionSchedules;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;

using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.Entities;

public partial class ChevronCoopDbContext //: DbContext
{
    public DbSet<DepositProduct> DepositProducts { get; set; }
    public DbSet<DepositProductCharge> DepositProductCharges { get; set; }
    public DbSet<DepositProductInterestRange> DepositProductInterestRanges { get; set; }
    public DbSet<DepositProductMasterView> DepositProductMasterView { get; set; }
    public DbSet<DepositProductChargeMasterView> DepositProductChargeMasterView { get; set; }
    public DbSet<DepositProductInterestRangeMasterView> DepositProductInterestRangeMasterView { get; set; }
    //public DbSet<ProductPublicationMasterView> ProductPublicationMasterView { get; set; }

    public DbSet<DepartmentDepositProductPublicationMasterView> DepartmentDepositProductPublicationMasterView { get; set; }

    public DbSet<DepartmentDepositProductPublication> DepartmentDepositProductPublications { get; set; }
    
    public DbSet<CustomerDepositProductPublication> CustomerDepositProductPublications { get; set; }

    public DbSet<CustomerDepositProductPublicationMasterView> CustomerDepositProductPublicationMasterView { get; set; }
    public DbSet<DepositAccountsMasterView> DepositAccountsMasterView { get; set; }
    public DbSet<DepositApplicationsMasterView> DepositApplicationsMasterView { get; set; }






    public DbSet<SavingsAccount> SavingsAccounts { get; set; }
    public DbSet<SavingsAccountApplication> SavingsAccountApplications { get; set; }
    public DbSet<SavingsAccountDeductionSchedule> SavingsAccountDeductionSchedules { get; set; }
    public DbSet<SavingsIncreaseDecrease> SavingsIncreaseDecreases { get; set; }
    public DbSet<SavingsCashAddition> SavingsCashAdditions { get; set; }


    public DbSet<SavingsAccountApplicationMasterView> SavingsAccountApplicationMasterView { get; set; }
    public DbSet<SavingsAccountMasterView> SavingsAccountMasterView { get; set; }
    public DbSet<SavingsCashAdditionMasterView> SavingsCashAdditionMasterView { get; set; }
    public DbSet<SavingsIncreaseDecreaseMasterView> SavingsIncreaseDecreaseMasterView { get; set; }
    public DbSet<SavingsAccountDeductionScheduleMasterView> SavingsAccountDeductionScheduleMasterView { get; set; }




    public DbSet<SpecialDepositAccountDeductionSchedule> SpecialDepositAccountDeductionSchedules { get; set; }
    public DbSet<SpecialDepositAccount> SpecialDepositAccounts { get; set; }
    public DbSet<SpecialDepositAccountApplication> SpecialDepositAccountApplications { get; set; }
    public DbSet<SpecialDepositCashAddition> SpecialDepositCashAdditions { get; set; }
    public DbSet<SpecialDepositFundTransfer> SpecialDepositFundTransfers { get; set; }
    public DbSet<SpecialDepositInterestAddition> SpecialDepositInterestAdditions { get; set; }
    public DbSet<SpecialDepositInterestSchedule> SpecialDepositInterestSchedules { get; set; }
    public DbSet<SpecialDepositInterestScheduleItem> SpecialDepositInterestScheduleItems { get; set; }
    public DbSet<SpecialDepositWithdrawal> SpecialDepositWithdrawals { get; set; }
    public DbSet<SpecialDepositIncreaseDecrease> SpecialDepositIncreaseDecreases { get; set; }

    public DbSet<SpecialDepositIncreaseDecreaseMasterView> SpecialDepositIncreaseDecreaseMasterView { get; set; }
    public DbSet<SpecialDepositWithdrawalMasterView> SpecialDepositWithdrawalMasterView { get; set; }
    public DbSet<SpecialDepositInterestScheduleMasterView> SpecialDepositInterestScheduleMasterView { get; set; }
    public DbSet<SpecialDepositInterestScheduleItemMasterView> SpecialDepositInterestScheduleItemMasterView { get; set; }
    public DbSet<SpecialDepositInterestAdditionMasterView> SpecialDepositInterestAdditionMasterView { get; set; }
    public DbSet<SpecialDepositFundTransferMasterView> SpecialDepositFundTransferMasterView { get; set; }
    public DbSet<SpecialDepositCashAdditionMasterView> SpecialDepositCashAdditionMasterView { get; set; }
    public DbSet<SpecialDepositAccountMasterView> SpecialDepositAccountMasterView { get; set; }
    public DbSet<SpecialDepositAccountDeductionScheduleMasterView> SpecialDepositAccountDeductionScheduleMasterView { get; set; }
    public DbSet<SpecialDepositAccountApplicationMasterView> SpecialDepositAccountApplicationMasterView { get; set; }


    public DbSet<FixedDepositAccount> FixedDepositAccounts { get; set; }
    public DbSet<FixedDepositAccountApplication> FixedDepositAccountApplications { get; set; }
    public DbSet<FixedDepositChangeInMaturity> FixedDepositChangeInMaturities { get; set; }
    public DbSet<FixedDepositInterestAddition> FixedDepositInterestAdditions { get; set; }
    public DbSet<FixedDepositLiquidation> FixedDepositLiquidations { get; set; }

    public DbSet<FixedDepositLiquidationCharge> FixedDepositLiquidationCharges { get; set; }
    public DbSet<FixedDepositInterestSchedule> FixedDepositInterestSchedules { get; set; }
    public DbSet<FixedDepositInterestScheduleItem> FixedDepositInterestScheduleItems { get; set; }

    public DbSet<FixedDepositAccountApplicationMasterView> FixedDepositAccountApplicationMasterView { get; set; }
    public DbSet<FixedDepositAccountMasterView> FixedDepositAccountMasterView { get; set; }
    public DbSet<FixedDepositChangeInMaturityMasterView> FixedDepositChangeInMaturityMasterView { get; set; }

    public DbSet<FixedDepositInterestAdditionMasterView> FixedDepositInterestAdditionMasterView { get; set; }
    public DbSet<FixedDepositInterestScheduleMasterView> FixedDepositInterestScheduleMasterView { get; set; }
    public DbSet<FixedDepositInterestScheduleItemMasterView> FixedDepositInterestScheduleItemMasterView { get; set; }
    public DbSet<FixedDepositLiquidationChargeMasterView> FixedDepositLiquidationChargeMasterView { get; set; }

    public DbSet<FixedDepositLiquidationMasterView> FixedDepositLiquidationMasterView { get; set; }
    public DbSet<SavingsActionsMasterView> SavingsActionsMasterView { get; set; }
    public DbSet<SpecialDepositActionsMasterView> SpecialDepositActionsMasterView { get; set; }
    public DbSet<FixedDepositActionsMasterView> FixedDepositActionsMasterView { get; set; }




    public void OnModelCreating_Deposits(ModelBuilder modelBuilder)
    {


        modelBuilder.ApplyConfiguration(new DepositProductConfiguration());
        modelBuilder.ApplyConfiguration(new DepositProductChargeConfiguration());
        modelBuilder.ApplyConfiguration(new DepositProductInterestRangeConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentDepositProductPublicationConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerDepositProductPublicationConfiguration());

        modelBuilder.ApplyConfiguration(new FixedDepositAccountConfiguration());
        modelBuilder.ApplyConfiguration(new FixedDepositAccountApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new FixedDepositChangeInMaturityConfiguration());
        modelBuilder.ApplyConfiguration(new FixedDepositInterestAdditionConfiguration());
        modelBuilder.ApplyConfiguration(new FixedDepositLiquidationConfiguration());
        modelBuilder.ApplyConfiguration(new FixedDepositInterestScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new FixedDepositInterestScheduleItemConfiguration());
        modelBuilder.ApplyConfiguration(new FixedDepositLiquidationChargeConfiguration());

        modelBuilder.ApplyConfiguration(new SavingsAccountConfiguration());
        modelBuilder.ApplyConfiguration(new SavingsAccountApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new SavingsAccountDeductionScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new SavingsIncreaseDecreaseConfiguration());
        modelBuilder.ApplyConfiguration(new SavingsCashAdditionConfiguration());

        modelBuilder.ApplyConfiguration(new SpecialDepositAccountDeductionScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialDepositAccountConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialDepositAccountApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialDepositCashAdditionConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialDepositFundTransferConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialDepositInterestAdditionConfiguration());

        modelBuilder.ApplyConfiguration(new SpecialDepositInterestScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialDepositInterestScheduleItemConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialDepositWithdrawalConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialDepositIncreaseDecreaseConfiguration());


        modelBuilder.Entity<DepositProductMasterView>(b =>
        {
            b.ToTable(nameof(DepositProductMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<SpecialDepositIncreaseDecreaseMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositIncreaseDecreaseMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<DepositProductChargeMasterView>(b =>
        {
            b.ToTable(nameof(DepositProductChargeMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<DepartmentDepositProductPublicationMasterView>(b =>
        {
            b.ToTable(nameof(DepartmentDepositProductPublicationMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<CustomerDepositProductPublicationMasterView>(b =>
        {
            b.ToTable(nameof(CustomerDepositProductPublicationMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<DepositProductMasterView>(b =>
        {
            b.ToTable(nameof(DepositProductMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<DepositProductChargeMasterView>(b =>
        {
            b.ToTable(nameof(DepositProductChargeMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<DepositProductInterestRangeMasterView>(b =>
        {
            b.ToTable(nameof(DepositProductInterestRangeMasterView), DbSchemaConstants.Deposits,
              t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<SavingsActionsMasterView>(b =>
        {
            b.ToTable(nameof(SavingsActionsMasterView), DbSchemaConstants.Deposits, buildAction: t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<SpecialDepositActionsMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositActionsMasterView), DbSchemaConstants.Deposits, buildAction: t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<FixedDepositActionsMasterView>(b =>
        {
            b.ToTable(nameof(FixedDepositActionsMasterView), DbSchemaConstants.Deposits, buildAction: t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<DepositApplicationsMasterView>(b =>
        {
            b.ToTable(nameof(DepositApplicationsMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<DepositProductInterestRangeMasterView>(b =>
        {
            b.ToTable(nameof(DepositProductInterestRangeMasterView), DbSchemaConstants.Deposits,
              t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<SpecialDepositWithdrawalMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositWithdrawalMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<SpecialDepositInterestScheduleMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositInterestScheduleMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<SpecialDepositInterestScheduleItemMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositInterestScheduleItemMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<SpecialDepositInterestAdditionMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositInterestAdditionMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<SpecialDepositFundTransferMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositFundTransferMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<SpecialDepositCashAdditionMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositCashAdditionMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<SpecialDepositAccountMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositAccountMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<SpecialDepositAccountDeductionScheduleMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositAccountDeductionScheduleMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<SpecialDepositAccountApplicationMasterView>(b =>
        {
            b.ToTable(nameof(SpecialDepositAccountApplicationMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
       

        modelBuilder.Entity<DepositAccountsMasterView>(b =>
        {
            b.ToTable(nameof(DepositAccountsMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<FixedDepositAccountApplicationMasterView>(b =>
        {
            b.ToTable(nameof(FixedDepositAccountApplicationMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });



        modelBuilder.Entity<FixedDepositAccountMasterView>(b =>
        {
            b.ToTable(nameof(FixedDepositAccountMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });


        modelBuilder.Entity<FixedDepositChangeInMaturityMasterView>(b =>
        {
            b.ToTable(nameof(FixedDepositChangeInMaturityMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<FixedDepositInterestAdditionMasterView>(b =>
        {
            b.ToTable(nameof(FixedDepositInterestAdditionMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<FixedDepositInterestScheduleMasterView>(b =>
        {
            b.ToTable(nameof(FixedDepositInterestScheduleMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<FixedDepositInterestScheduleItemMasterView>(b =>
        {
            b.ToTable(nameof(FixedDepositInterestScheduleItemMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<FixedDepositLiquidationMasterView>(b =>
        {
            b.ToTable(nameof(FixedDepositLiquidationMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });


        modelBuilder.Entity<SavingsAccountApplicationMasterView>(b =>
        {
            b.ToTable(nameof(SavingsAccountApplicationMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<SavingsAccountMasterView>(b =>
        {
            b.ToTable(nameof(SavingsAccountMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });


        modelBuilder.Entity<SavingsCashAdditionMasterView>(b =>
        {
            b.ToTable(nameof(SavingsCashAdditionMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });


        modelBuilder.Entity<SavingsIncreaseDecreaseMasterView>(b =>
        {
            b.ToTable(nameof(SavingsIncreaseDecreaseMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });


        modelBuilder.Entity<SavingsAccountDeductionScheduleMasterView>(b =>
        {
            b.ToTable(nameof(SavingsAccountDeductionScheduleMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<FixedDepositLiquidationChargeMasterView>(b =>
        {
            b.ToTable(nameof(FixedDepositLiquidationChargeMasterView), DbSchemaConstants.Deposits, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
    }


}