using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Accounting.LedgerAccounts;

public partial class LedgerAccountConfiguration : BaseEntityConfiguration<LedgerAccount, string>
{
  public override void Configure(EntityTypeBuilder<LedgerAccount> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(LedgerAccount), DbSchemaConstants.Accounting);

    entity.HasIndex(x => new { x.Name }).IsUnique();
    entity.HasIndex(x => new { x.Name, x.Code }).IsUnique();

    entity.Property(a => a.AccountType)
      .HasConversion<string>()
      .HasMaxLength(100)
      .IsRequired();

    entity.Property(a => a.UOM)
      .HasMaxLength(100)
      .IsRequired()
      .HasConversion<string>();

    entity.Property(a => a.Code)
      .HasColumnType("nvarchar")
      .HasMaxLength(100)
      .IsRequired();

    entity.Property(a => a.Name)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired();

    entity.Property(a => a.ParentId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40);

    entity.HasOne(a => a.Parent)
      .WithMany(a => a.Children)
      .HasForeignKey(a => a.ParentId);

    entity.Property(a => a.ClearedBalance)
      .HasDefaultValue(0);

    entity.Property(a => a.UnclearedBalance)
      .HasDefaultValue(0);

    entity.Property(a => a.LedgerBalance)
      .HasDefaultValue(0);

    entity.Property(a => a.AvailableBalance)
      .HasDefaultValue(0);

    entity.Property(a => a.IsOfficeAccount)
      .HasDefaultValue(false);

    entity.Property(a => a.AllowManualEntry)
      .HasDefaultValue(true);

    entity.Property(a => a.IsClosed)
      .HasDefaultValue(false)
      .IsRequired();

    entity.Property(a => a.DateClosed);

    entity.Property(a => a.ClosedByUserName)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);
    
    entity.Property(a => a.CurrencyId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40)
      .IsRequired();
            
    entity.HasOne(a => a.Currency)
      .WithMany()
      .HasForeignKey(a => a.CurrencyId);

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<LedgerAccount> entity);
}