using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.MasterData.Currencies;

public partial class CurrencyConfiguration : BaseEntityConfiguration<Currency, string>
{
  public override void Configure(EntityTypeBuilder<Currency> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(Currency), DbSchemaConstants.MasterData);
            
    entity.Property(c => c.Code)
      .HasColumnType("nvarchar")
      .HasMaxLength(8)
      .IsRequired();

    entity.Property(c => c.Name)
      .HasColumnType("nvarchar")
      .HasMaxLength(64)
      .IsRequired();

    entity.Property(c => c.Symbol)
      .HasColumnType("nvarchar")
      .HasMaxLength(8)
      .IsRequired();

    entity.Property(c => c.IsoSymbol)
      .HasColumnType("nvarchar")
      .HasMaxLength(10);

    entity.Property(c => c.DecimalPlaces)
      .IsRequired();

    entity.Property(c => c.Format)
      .HasColumnType("nvarchar")
      .HasMaxLength(16);


    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<Currency> entity);
}