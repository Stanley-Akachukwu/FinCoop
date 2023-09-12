using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Accounting.AccountingPeriods;

public partial class AccountingPeriodConfiguration : BaseEntityConfiguration<AccountingPeriod, string>
{
  public override void Configure(EntityTypeBuilder<AccountingPeriod> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(AccountingPeriod), DbSchemaConstants.Accounting);

    entity.HasIndex(x => new { x.Name }).IsUnique();

    entity.Property(a => a.Name)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

    entity.Property(a => a.StartDate)
      .IsRequired();

    entity.Property(a => a.EndDate)
      .IsRequired();

    entity.Property(a => a.IsCurrent).HasDefaultValue(false);

    entity.Property(a => a.IsClosed).HasDefaultValue(false);

    entity.Property(a => a.ClosedByUserName)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

    entity.Property(a => a.DateClosed);
            
            
    entity.Property(e => e.CalendarId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40)
      .IsRequired();

    entity.HasOne(a => a.Calendar)
      .WithMany(e => e.AccountingPeriods)
      .HasForeignKey(a => a.CalendarId)
      .OnDelete(DeleteBehavior.Restrict);


    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<AccountingPeriod> entity);
}