using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Accounting.PaymentModes;

public partial class PaymentModeConfiguration : BaseEntityConfiguration<PaymentMode, string>
{
  public override void Configure(EntityTypeBuilder<PaymentMode> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(PaymentMode), DbSchemaConstants.Accounting);

    entity.HasIndex(x => new { x.Name }).IsUnique();
    
    entity.Property(p => p.Name)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired();

    entity.Property(p => p.Channel)
      .HasMaxLength(100)
      .HasColumnType("nvarchar")
      .IsRequired()
      .HasDefaultValue(PaymentChannel.CASH)
      .HasConversion<string>();

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<PaymentMode> entity);
}