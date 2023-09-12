using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Employees;
using AP.ChevronCoop.Entities.Security.Approvals;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.Entities
{
    public partial class ChevronCoopDbContext //: DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeMasterView> EmployeeMasterView { get; set; }
      
        public void OnModelCreating_Employees(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            modelBuilder.Entity<EmployeeMasterView>(b =>
            {
                b.ToTable(nameof(EmployeeMasterView), DbSchemaConstants.HumanResources, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
            
        }

    }
}
