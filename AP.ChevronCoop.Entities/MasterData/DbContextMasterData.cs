using AP.ChevronCoop.Entities.MasterData.Banks;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.MasterData.Departments;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities.MasterData.Locations;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.Entities
{
    public partial class ChevronCoopDbContext //: DbContext
    {


        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankMasterView> BankMasterView { get; set; }
        
        public DbSet<Charge> Charges { get; set; }
        public DbSet<ChargeMasterView> ChargeMasterView { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentMasterView> DepartmentMasterView { get; set; }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyMasterView> CurrencyMasterView { get; set; }
        public DbSet<GlobalCode> GlobalCodes { get; set; }
        public DbSet<GlobalCodeMasterView> GlobalCodeMasterView { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationMasterView> LocationMasterView { get; set; }

        public void OnModelCreating_MasterData(ModelBuilder modelBuilder)
        {
           // base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new BankConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new GlobalCodeConfiguration());
            modelBuilder.ApplyConfiguration(new ChargeConfiguration());

            modelBuilder.Entity<CurrencyMasterView>(b =>
            {
                b.ToTable(nameof(CurrencyMasterView), DbSchemaConstants.MasterData, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<BankMasterView>(b =>
            {
                b.ToTable(nameof(BankMasterView), DbSchemaConstants.MasterData, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DepartmentMasterView>(b =>
            {
                b.ToTable(nameof(DepartmentMasterView), DbSchemaConstants.MasterData, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<GlobalCodeMasterView>(b =>
            {
                b.ToTable(nameof(GlobalCodeMasterView), DbSchemaConstants.MasterData, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<LocationMasterView>(b =>
            {
                b.ToTable(nameof(LocationMasterView), DbSchemaConstants.MasterData, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
            
            modelBuilder.Entity<ChargeMasterView>(b =>
            {
                b.ToTable(nameof(ChargeMasterView), DbSchemaConstants.MasterData, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });

        }

    }
}