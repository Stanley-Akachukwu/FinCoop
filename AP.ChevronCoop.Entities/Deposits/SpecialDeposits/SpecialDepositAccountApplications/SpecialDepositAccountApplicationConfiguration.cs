using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;

public partial class SpecialDepositAccountApplicationConfiguration : BaseEntityConfiguration<SpecialDepositAccountApplication, string>
{
    public override void Configure(EntityTypeBuilder<SpecialDepositAccountApplication> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(SpecialDepositAccountApplication), DbSchemaConstants.Deposits);

        entity.HasIndex(x => new { x.ApplicationNo }).IsUnique();
        entity.HasIndex(x => new { x.DepositProductId });
        entity.HasIndex(x => new { x.CustomerId });


        entity.Property(e => e.ApplicationNo)
          .HasColumnType("nvarchar")
          .HasMaxLength(50);

        entity.HasOne(e => e.DepositProduct)
          .WithMany()
          .HasForeignKey(e => e.DepositProductId);

        entity.HasOne(e => e.Customer)
          .WithMany()
          .HasForeignKey(e => e.CustomerId);

        entity.Property(e => e.PaymentDocumentId).HasColumnType("nvarchar").HasMaxLength(40);
        entity.HasOne(e => e.PaymentDocument)
         .WithMany()
         .HasForeignKey(e => e.PaymentDocumentId);


        entity.HasOne(e => e.Approval)
           .WithMany()
           .HasForeignKey(e => e.ApprovalId)
           .IsRequired(false);


        entity.Property(e => e.Amount)
         .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

        entity.Property(e => e.InterestRate)
          .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();


        entity.Property(e => e.PaymentAccountNumber)
           .HasColumnType("nvarchar")
           .HasMaxLength(50);

        entity.Property(e => e.PaymentBankName)
            .HasColumnType("nvarchar")
            .HasMaxLength(100);

        entity.Property(e => e.ModeOfPayment).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<SpecialDepositAccountApplication> entity);
}
