using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Employees;

public partial class EmployeeConfiguration : BaseEntityConfiguration<Employee, string>
{
  public override void Configure(EntityTypeBuilder<Employee> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(Employee), DbSchemaConstants.HumanResources);

    entity.HasIndex(x => new { x.EmployeeNo }).IsUnique();

    entity.Property(e => e.EmployeeNo)
      .HasColumnType("nvarchar")
      .HasMaxLength(50)
      .IsRequired();
    
    entity.Property(e => e.LastName)
      .HasColumnType("nvarchar")
      .HasMaxLength(50)
      .IsRequired();
    
    entity.Property(e => e.MiddleName)
      .HasColumnType("nvarchar")
      .HasMaxLength(50);
    
    entity.Property(e => e.FirstName)
      .HasColumnType("nvarchar")
      .HasMaxLength(50)
      .IsRequired();
    
    entity.Property(e => e.Dob);
    entity.Property(e => e.Gender)
      .HasColumnType("nvarchar")
      .HasMaxLength(32)
      .IsRequired()
      .HasConversion<string>();
    
    entity.Property(e => e.ProfileImageUrl)
      .HasColumnType("nvarchar")
      .HasMaxLength(256);
    
    entity.Property(e => e.EmploymentDate);
    
    entity.Property(e => e.CustomerId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40);

    entity.HasOne(e => e.Customer).WithMany().HasForeignKey(e => e.CustomerId);
    


    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<Employee> entity);
}