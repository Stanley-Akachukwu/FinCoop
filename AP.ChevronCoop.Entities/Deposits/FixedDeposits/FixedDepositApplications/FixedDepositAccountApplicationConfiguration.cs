using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications
{

    public partial class FixedDepositAccountApplicationConfiguration : BaseEntityConfiguration<FixedDepositAccountApplication, string>
    {
        public override void Configure(EntityTypeBuilder<FixedDepositAccountApplication> entity)
        {
            base.Configure(entity);
            entity.ToTable(nameof(FixedDepositAccountApplication), DbSchemaConstants.Deposits);

            entity.HasIndex(x => new { x.ApplicationNo }).IsUnique();
            entity.HasIndex(x => new { x.DepositProductId });
            entity.HasIndex(x => new { x.CustomerId });


            entity.Property(e => e.ApplicationNo)
              .HasColumnType("nvarchar")
              .HasMaxLength(100)
              .IsRequired();


            entity.Property(e => e.DepositProductId);
            entity.HasOne(e => e.DepositProduct)
              .WithMany()
              .HasForeignKey(e => e.DepositProductId)
              .IsRequired();


            entity.Property(e => e.CustomerId);
            entity.HasOne(e => e.Customer)
              .WithMany()
              .HasForeignKey(e => e.CustomerId)
              .IsRequired();

            entity.Property(e => e.Amount).HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            entity.Property(c => c.TenureUnit).HasMaxLength(100)
              .IsRequired().HasDefaultValue(Tenure.NONE).HasConversion<string>();
            entity.Property(e => e.TenureValue).HasDefaultValue(0).IsRequired();



            entity.Property(e => e.InterestRate)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            //entity.Property(e => e.PaymentAccountNumber)
            //  .HasColumnType("nvarchar")
            //  .HasMaxLength(32);

            //entity.Property(e => e.PaymentBankName)
            //    .HasColumnType("nvarchar")
            //    .HasMaxLength(100);


            entity.Property(c => c.MaturityInstructionType).HasMaxLength(100).IsRequired().HasConversion<string>();


            entity.Property(e => e.LiquidationAccountType).HasMaxLength(100).IsRequired().HasConversion<string>();

            entity.Property(e => e.ModeOfPayment).HasMaxLength(100).IsRequired().HasConversion<string>();

            entity.HasOne(e => e.SavingsLiquidationAccount)
             .WithMany()
             .HasForeignKey(e => e.SavingsLiquidationAccountId)
             .IsRequired(false);

            entity.HasOne(e => e.SpecialDepositLiquidationAccount)
             .WithMany()
             .HasForeignKey(e => e.SpecialDepositLiquidationAccountId)
             .IsRequired(false);

            entity.HasOne(e => e.CustomerBankLiquidationAccount)
             .WithMany()
             .HasForeignKey(e => e.CustomerBankLiquidationAccountId)
             .IsRequired(false);

            entity.HasOne(e => e.SpecialDepositFundingSourceAccount)
             .WithMany()
             .HasForeignKey(e => e.SpecialDepositFundingSourceAccountId)
             .IsRequired(false);

            entity.HasOne(e => e.CustomerBankFundingSourceAccount)
             .WithMany()
             .HasForeignKey(e => e.CustomerBankFundingSourceAccountId)
             .IsRequired(false);

            entity.HasOne(e => e.PaymentDocument)
             .WithMany()
             .HasForeignKey(e => e.PaymentDocumentId)
             .IsRequired(false);


            entity.HasOne(e => e.Approval)
            .WithMany()
            .HasForeignKey(e => e.ApprovalId)
            .IsRequired(false);


            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<FixedDepositAccountApplication> entity);
    }
}
