using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanApplications;

public partial class LoanApplicationConfiguration : BaseEntityConfiguration<LoanApplication, string>
{
    public override void Configure(EntityTypeBuilder<LoanApplication> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanApplication), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.ApplicationNo }).IsUnique();
        entity.HasIndex(x => new { x.LoanProductId });
        entity.HasIndex(x => new { x.CustomerId });


        entity.Property(e => e.ApplicationNo)
          .HasColumnType("nvarchar")
          .HasMaxLength(64)
          .IsRequired();
        
        entity.Property(e => e.AccountNo)
          .HasColumnType("nvarchar")
          .HasMaxLength(64)
          .IsRequired(false);
        
        entity.Property(e => e.QualificationTargetProductId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40)
          .IsRequired(false);

        entity.Property(lpc => lpc.LoanProductId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.LoanProduct)
          .WithMany()
          .HasForeignKey(e => e.LoanProductId)
          .IsRequired();

        entity.Property(lpc => lpc.CustomerId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.Customer)
          .WithMany()
          .HasForeignKey(e => e.CustomerId)
          .IsRequired();

        entity.Property(lpc => lpc.ApprovalId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.Approval)
          .WithMany()
          .HasForeignKey(e => e.ApprovalId)
          .IsRequired(false);

        entity.Property(lpc => lpc.SpecialDepositId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.SpecialDeposit)
           .WithMany()
           .HasForeignKey(e => e.SpecialDepositId)
           .IsRequired(false);

        entity.Property(lpc => lpc.CustomerDisbursementAccountId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.CustomerDisbursementAccount)
           .WithMany()
           .HasForeignKey(e => e.CustomerDisbursementAccountId)
           .IsRequired(false);

        entity.Property(e => e.Principal)
          .HasPrecision(18,2)
          .HasDefaultValue(0.00)    
          .IsRequired();

        entity.Property(e => e.TenureUnit).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.QualificationTargetProductType).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanApplications.LoanApplication> entity);
}