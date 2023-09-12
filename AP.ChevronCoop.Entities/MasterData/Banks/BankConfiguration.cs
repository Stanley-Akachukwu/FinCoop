using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.MasterData.Banks;

public partial class BankConfiguration : BaseEntityConfiguration<Bank, string>
{
  public override void Configure(EntityTypeBuilder<Bank> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(Bank), DbSchemaConstants.MasterData);

    entity.HasIndex(x => new { x.Name }).IsUnique();
    entity.HasIndex(x => new { x.Name, x.Code }).IsUnique();

    entity.Property(e => e.Code)
      .HasColumnType("nvarchar")
      .HasMaxLength(16)
      .IsRequired();

    entity.Property(e => e.Name)
      .HasColumnType("nvarchar")
      .HasMaxLength(64)
      .IsRequired();

    entity.Property(e => e.Address)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

    entity.Property(e => e.ContactName)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

    entity.Property(e => e.ContactDetails)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<Bank> entity);
}