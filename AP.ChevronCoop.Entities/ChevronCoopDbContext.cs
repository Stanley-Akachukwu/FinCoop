using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserTokens;
using Audit.EntityFramework;

namespace AP.ChevronCoop.Entities
{


    public partial class ChevronCoopDbContext :
    AuditIdentityDbContext<
        ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>   // DbContext
    {
        public ChevronCoopDbContext() : base()
        {
        }

        public ChevronCoopDbContext(DbContextOptions<ChevronCoopDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.HasSequence<long>($"{SequenceNames.Asset.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>($"{SequenceNames.Equity.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>($"{SequenceNames.Expense.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>($"{SequenceNames.Liability.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>($"{SequenceNames.Income.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>($"{SequenceNames.COGS.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>($"{SequenceNames.Loans.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>($"{SequenceNames.Deposits.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>($"{SequenceNames.Withdrawals.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>($"{SequenceNames.Repayments.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>($"{SequenceNames.General.ToString()}", schema: DbSchemaConstants.Core).StartsAt(1000).IncrementsBy(1);


            this.OnModelCreating_MasterData(modelBuilder);
            this.OnModelCreating_Documents(modelBuilder);
            this.OnModelCreating_Accounting(modelBuilder);
            this.OnModelCreating_Loans(modelBuilder);
            this.OnModelCreating_Deposits(modelBuilder);
            this.OnModelCreating_Security(modelBuilder);
            this.OnModelCreating_Security_Approvals(modelBuilder);
            this.OnModelCreating_Employees(modelBuilder);
            this.OnModelCreating_Customers(modelBuilder);
            this.OnModelCreating_Payrolls(modelBuilder);


            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
             .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            // base.OnModelCreating(modelBuilder);

        }


        public override int SaveChanges()
        {
            //var entries = ChangeTracker
            //    .Entries()
            //    .Where(e => e.Entity is BaseEntity && (
            //            e.State == EntityState.Added
            //            || e.State == EntityState.Modified));

            //foreach (var entityEntry in entries)
            //{
            //    ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

            //    if (entityEntry.State == EntityState.Added)
            //    {
            //        ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
            //    }
            //}

            return base.SaveChanges();
        }
    }

}