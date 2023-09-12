using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;

public partial class LoanApplicationGuarantorConfiguration : BaseEntityConfiguration<LoanApplicationGuarantor, string>
{
    public override void Configure(EntityTypeBuilder<LoanApplicationGuarantor> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanApplicationGuarantor), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.LoanApplicationId });
        entity.HasIndex(x => new { x.GuarantorId });

        entity.Property(lpc => lpc.LoanApplicationId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.LoanApplication)
          .WithMany()
          .HasForeignKey(e => e.LoanApplicationId)
          .IsRequired();
        
        entity.Property(e => e.Comment)
          .HasColumnType("nvarchar")
          .HasMaxLength(2048)
          .IsRequired(false);

        entity.Property(lpc => lpc.GuarantorId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.Guarantor)
          .WithMany()
          .HasForeignKey(e => e.GuarantorId)
          .IsRequired();

        entity.Property(e => e.GuarantorType).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.GuarantorApprovalType).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();
        // entity.Property(e => e.Status).HasMaxLength(32).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanApplicationGuarantor> entity);
}