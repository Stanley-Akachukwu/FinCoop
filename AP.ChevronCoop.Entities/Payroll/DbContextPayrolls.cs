using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AP.ChevronCoop.Entities.Payroll;
using AP.ChevronCoop.Entities.Payroll.PayrollCronJobConfigurations;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionScheduleItems;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.Entities;

public partial class ChevronCoopDbContext //: DbContext
{
    public DbSet<PayrollDeductionItem> PayrollDeductionItems { get; set; }
    public DbSet<PayrollDeductionItemMasterView> PayrollDeductionItemMasterView { get; set; }
    public DbSet<PayrollDeductionSchedule> PayrollDeductionSchedules { get; set; }
    public DbSet<PayrollDeductionScheduleMasterView> PayrollDeductionScheduleMasterView { get; set; }
    public DbSet<PayrollDeductionScheduleItem> PayrollDeductionScheduleItems { get; set; }
    public DbSet<PayrollDeductionScheduleItemMasterView> PayrollDeductionScheduleItemMasterView { get; set; }
    public DbSet<PayrollCronJobConfig> PayrollCronJobConfigs { get; set; }
    public DbSet<PayrollCronJobConfigMasterView> PayrollCronJobConfigMasterView { get; set; }

    public void OnModelCreating_Payrolls(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PayrollDeductionItemConfiguration());
        modelBuilder.ApplyConfiguration(new PayrollDeductionScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new PayrollDeductionScheduleItemConfiguration());
        modelBuilder.ApplyConfiguration(new PayrollCronJobConfigConfiguration());

        modelBuilder.Entity<PayrollDeductionItemMasterView>(b =>
        {
            b.ToTable(nameof(PayrollDeductionItemMasterView), DbSchemaConstants.Payroll, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });


        modelBuilder.Entity<PayrollDeductionScheduleMasterView>(b =>
        {
            b.ToTable(nameof(PayrollDeductionScheduleMasterView), DbSchemaConstants.Payroll, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<PayrollDeductionScheduleItemMasterView>(b =>
        {
            b.ToTable(nameof(PayrollDeductionScheduleItemMasterView), DbSchemaConstants.Payroll, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<PayrollCronJobConfigMasterView>(b =>
        {
            b.ToTable(nameof(PayrollCronJobConfigMasterView), DbSchemaConstants.Payroll, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        

       
    }
}





