using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Accounting.FinancialCalendars;

public partial class FinancialCalendarConfiguration : BaseEntityConfiguration<FinancialCalendar, string>
{
  public override void Configure(EntityTypeBuilder<FinancialCalendar> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(FinancialCalendar), DbSchemaConstants.Accounting);

    entity.HasIndex(x => new { x.Name }).IsUnique();
    entity.HasIndex(x => new { x.Name, x.Code }).IsUnique();

    entity.Property(e => e.Code)
      .HasColumnType("nvarchar")
      .HasMaxLength(20)
      .IsRequired();

    entity.Property(e => e.Name)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired();

    entity.Property(e => e.StartDate)
      .IsRequired();

    entity.Property(e => e.EndDate)
      .IsRequired();

    entity.Property(e => e.IsCurrent);

    entity.Property(e => e.IsClosed);

    entity.Property(e => e.ClosedByUserName)
      .HasColumnType("nvarchar")
      .HasMaxLength(64);

    entity.Property(e => e.DateClosed);


    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<FinancialCalendar> entity);
}