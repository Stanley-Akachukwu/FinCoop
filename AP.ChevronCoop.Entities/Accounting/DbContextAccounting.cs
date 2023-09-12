using AP.ChevronCoop.Entities.Accounting.AccountingPeriods;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Accounting.FinancialCalendars;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Accounting.LienTypes;
using AP.ChevronCoop.Entities.Accounting.PaymentModes;
using AP.ChevronCoop.Entities.Accounting.TransactionDocuments;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.Entities
{
    public partial class ChevronCoopDbContext //: DbContext
    {

        public DbSet<CompanyBankAccount> CompanyBankAccounts { get; set; }
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public DbSet<FinancialCalendar> FinancialCalendars { get; set; }
        public DbSet<AccountingPeriod> AccountingPeriods { get; set; }
        public DbSet<TransactionJournal> TransactionJournals { get; set; }
        public DbSet<TransactionDocument> TransactionDocuments { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<LienType> LienTypes { get; set; }
        
        
        public DbSet<CompanyBankAccountMasterView> CompanyBankAccountMasterView { get; set; }
        public DbSet<LedgerAccountMasterView> LedgerAccountMasterView { get; set; }
        public DbSet<FinancialCalendarMasterView> FinancialCalendarMasterView { get; set; }
        public DbSet<AccountingPeriodMasterView> AccountingPeriodMasterView { get; set; }
        public DbSet<TransactionJournalMasterView> TransactionJournalMasterView { get; set; }
        public DbSet<TransactionDocumentMasterView> TransactionDocumentMasterView { get; set; }
        public DbSet<JournalEntryMasterView> JournalEntryMasterView { get; set; }
        public DbSet<PaymentModeMasterView> PaymentModeMasterView { get; set; }
        public DbSet<LienTypeMasterView> LienTypeMasterView { get; set; }


        public void OnModelCreating_Accounting(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyBankAccountConfiguration());
            modelBuilder.ApplyConfiguration(new LedgerAccountConfiguration());
            modelBuilder.ApplyConfiguration(new FinancialCalendarConfiguration());
            modelBuilder.ApplyConfiguration(new AccountingPeriodConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionJournalConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new JournalEntryConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentModeConfiguration());
            modelBuilder.ApplyConfiguration(new LienTypeConfiguration());


            modelBuilder.Entity<AccountingPeriodMasterView>(b =>
            {
                b.ToTable(nameof(AccountingPeriodMasterView), DbSchemaConstants.Accounting, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
            
            modelBuilder.Entity<CompanyBankAccountMasterView>(b =>
            {
                b.ToTable(nameof(CompanyBankAccountMasterView), DbSchemaConstants.Accounting, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<FinancialCalendarMasterView>(b =>
            {
                b.ToTable(nameof(FinancialCalendarMasterView), DbSchemaConstants.Accounting, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
            
            modelBuilder.Entity<PaymentModeMasterView>(b =>
            {
                b.ToTable(nameof(PaymentModeMasterView), DbSchemaConstants.Accounting, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
            
            modelBuilder.Entity<TransactionDocumentMasterView>(b =>
            {
                b.ToTable(nameof(TransactionDocumentMasterView), DbSchemaConstants.Accounting, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });


            modelBuilder.Entity<TransactionJournalMasterView>(b =>
            {
                b.ToTable(nameof(TransactionJournalMasterView), DbSchemaConstants.Accounting, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
            
            modelBuilder.Entity<JournalEntryMasterView>(b =>
            {
                b.ToTable(nameof(JournalEntryMasterView), DbSchemaConstants.Accounting, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
            
            modelBuilder.Entity<LedgerAccountMasterView>(b =>
            {
                b.ToTable(nameof(LedgerAccountMasterView), DbSchemaConstants.Accounting, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
            
            modelBuilder.Entity<LienTypeMasterView>(b =>
            {
                b.ToTable(nameof(LienTypeMasterView), DbSchemaConstants.Accounting, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
        }

    }
}