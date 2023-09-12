using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanApplicationApprovals;

public partial class LoanApplicationApprovalConfiguration : BaseEntityConfiguration<LoanApplicationApproval, string>
{
    public override void Configure(EntityTypeBuilder<LoanApplicationApproval> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanApplicationApproval), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.LoanApplicationId });

        entity.Property(lpc => lpc.LoanApplicationId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.LoanApplication)
           .WithMany()
           .HasForeignKey(e => e.LoanApplicationId)
           .IsRequired();
        
        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanApplicationApproval> entity);
}