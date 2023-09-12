using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanApplicationItems;

public partial class LoanApplicationItemConfiguration : BaseEntityConfiguration<LoanApplicationItem, string>
{
    public override void Configure(EntityTypeBuilder<LoanApplicationItem> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanApplicationItem), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.LoanApplicationId });
        entity.HasIndex(x => new { x.Name, x.Model, x.BrandName, x.Color });

        entity.Property(e => e.Name)
          .HasColumnType("nvarchar")
          .HasMaxLength(32)
          .IsRequired();

        entity.Property(e => e.Model)
          .HasColumnType("nvarchar")
          .HasMaxLength(32)
          .IsRequired();

        entity.Property(e => e.BrandName)
          .HasColumnType("nvarchar")
          .HasMaxLength(32);

        entity.Property(e => e.Color)
          .HasColumnType("nvarchar")
          .HasMaxLength(16);

        entity.Property(lpc => lpc.LoanApplicationId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.LoanApplication)
          .WithMany()
          .HasForeignKey(e => e.LoanApplicationId)
          .IsRequired();

        entity.Property(e => e.Amount)
          .HasColumnType("decimal(18,2)")
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(e => e.ItemType).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanApplicationItems.LoanApplicationItem> entity);
}