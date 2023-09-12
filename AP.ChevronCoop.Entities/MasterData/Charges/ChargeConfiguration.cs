using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.MasterData.Charges;

public partial class ChargeConfiguration : BaseEntityConfiguration<Charge, string>
{
    public override void Configure(EntityTypeBuilder<Charge> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(Charge), DbSchemaConstants.MasterData);

        entity.HasIndex(x => new { x.Code }).IsUnique();
        entity.HasIndex(x => new { x.Code, x.Name }).IsUnique();

        entity.Property(c => c.Code)
          .HasColumnType("nvarchar")
          .HasMaxLength(32)
          .IsRequired();

        entity.Property(c => c.Name)
          .HasColumnType("nvarchar")
          .HasMaxLength(128)
          .IsRequired();

        entity.Property(c => c.ChargeValue)
      .HasDefaultValue(0).IsRequired();

      //  entity.Property(c => c.FlatFee)
      //.HasDefaultValue(0);

      //  entity.Property(c => c.Percent)
      //    .HasDefaultValue(0);

        entity.Property(c => c.MaximumCharge);

        entity.Property(c => c.MinimimumCharge);

        entity.Property(c => c.Method).HasMaxLength(100)
          .IsRequired()
          .HasDefaultValue(ChargeMethod.FLAT)
          .HasConversion<string>();

        entity.Property(c => c.Target)
          .IsRequired()
          .HasDefaultValue(ChargeTarget.VALUE)
          .HasConversion<string>();

        entity.Property(c => c.CalculationMethod).HasMaxLength(100)
          .IsRequired()
          .HasDefaultValue(ChargeCalculationMethod.ADD)
          .HasConversion<string>();

        entity.Property(c => c.CurrencyId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40)
          .IsRequired();

        entity.HasOne(c => c.Currency)
          .WithMany()
          .HasForeignKey(c => c.CurrencyId)
          .OnDelete(DeleteBehavior.Restrict);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Charge> entity);
}