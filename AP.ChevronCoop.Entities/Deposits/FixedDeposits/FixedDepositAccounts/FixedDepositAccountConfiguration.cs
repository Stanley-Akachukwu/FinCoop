using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts
{
    public partial class FixedDepositAccountConfiguration : BaseEntityConfiguration<FixedDepositAccount, string>
    {
        public override void Configure(EntityTypeBuilder<FixedDepositAccount> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(FixedDepositAccount), DbSchemaConstants.Deposits);

            //entity.HasIndex(x => new { x.ApplicationNo }).IsUnique();
            entity.HasIndex(x => new { x.DepositProductId });
            entity.HasIndex(x => new { x.CustomerId });
            entity.HasIndex(x => new { x.AccountNo }).IsUnique();

            entity.Property(e => e.AccountNo)
             .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired();

            entity.Property(e => e.DepositProductId).HasColumnType("nvarchar").HasMaxLength(40);
            entity.HasOne(e => e.DepositProduct)
              .WithMany()
              .HasForeignKey(e => e.DepositProductId)
              .IsRequired();

            entity.Property(e => e.CustomerId).HasColumnType("nvarchar").HasMaxLength(40); ;
            entity.HasOne(e => e.Customer)
              .WithMany()
              .HasForeignKey(e => e.CustomerId)
              .IsRequired();

            entity.Property(e => e.DepositAccountId).HasColumnType("nvarchar").HasMaxLength(40); ;
            entity.HasOne(e => e.DepositAccount)
             .WithMany()
             .HasForeignKey(e => e.DepositAccountId)
             .IsRequired();

            entity.Property(e => e.ChargesAccruedAccountId).HasColumnType("nvarchar").HasMaxLength(40); ;
            entity.HasOne(e => e.ChargesAccruedAccount)
            .WithMany()
            .HasForeignKey(e => e.ChargesAccruedAccountId)
            .IsRequired();


            entity.Property(e => e.ChargesIncomeAccountId).HasColumnType("nvarchar").HasMaxLength(40); ;
            entity.HasOne(e => e.ChargesIncomeAccount)
            .WithMany()
            .HasForeignKey(e => e.ChargesIncomeAccountId)
            .IsRequired();

            entity.Property(e => e.InterestEarnedAccountId).HasColumnType("nvarchar").HasMaxLength(40); ;
            entity.HasOne(e => e.InterestEarnedAccount)
            .WithMany()
            .HasForeignKey(e => e.InterestEarnedAccountId)
            .IsRequired();

            entity.Property(e => e.InterestPayoutAccountId).HasColumnType("nvarchar").HasMaxLength(40); ;
            entity.HasOne(e => e.InterestPayoutAccount)
            .WithMany()
            .HasForeignKey(e => e.InterestPayoutAccountId)
            .IsRequired();
            
            entity.Property(lpc => lpc.ChargesWaivedAccountId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40);

            entity.HasOne(e => e.ChargesWaivedAccount)
              .WithMany()
              .HasForeignKey(e => e.ChargesWaivedAccountId)
              .IsRequired();

            entity.HasMany(e => e.FixedDepositLiquidations)
                 .WithOne(e => e.FixedDepositAccount);


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


            entity.HasOne(e => e.RootParentAccount)
            .WithMany()
            .HasForeignKey(e => e.RootParentAccountId);

            entity.HasOne(e => e.ParentAccount)
              .WithMany()
              .HasForeignKey(e => e.ParentAccountId);
  

            entity.Property(e => e.MaturityInstructionType).HasMaxLength(100).IsRequired().HasConversion<string>();
            entity.Property(e => e.LiquidationAccountType).HasMaxLength(100).IsRequired().HasConversion<string>();

            entity.Property(e => e.CustomerBankLiquidationAccountId).HasColumnType("nvarchar").HasMaxLength(40); ;
            entity.HasOne(e => e.CustomerBankLiquidationAccount)
            .WithMany()
            .HasForeignKey(e => e.CustomerBankLiquidationAccountId)
            .IsRequired(false);

            entity.Property(e => e.SavingsLiquidationAccountId).HasColumnType("nvarchar").HasMaxLength(40); ;
            entity.HasOne(e => e.SavingsLiquidationAccount)
            .WithMany()
            .HasForeignKey(e => e.SavingsLiquidationAccountId)
            .IsRequired(false);

            entity.Property(e => e.SpecialDepositLiquidationAccountId).HasColumnType("nvarchar").HasMaxLength(40); ;
            entity.HasOne(e => e.SpecialDepositLiquidationAccount)
            .WithMany()
            .HasForeignKey(e => e.SpecialDepositLiquidationAccountId)
            .IsRequired(false);


            entity.Property(e => e.ClosedByUserId);
            entity.HasOne(e => e.ClosedByUser)
            .WithMany()
            .HasForeignKey(e => e.ClosedByUserId)
            .IsRequired(false);

            entity.Property(e => e.MinimumBalanceLimit)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            entity.Property(e => e.MaximumBalanceLimit)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            entity.Property(e => e.SingleWithdrawalLimit)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            entity.Property(e => e.DailyWithdrawalLimit)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            entity.Property(e => e.WeeklyWithdrawalLimit)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

            entity.Property(e => e.MonthlyWithdrawalLimit)
              .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();





            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<FixedDepositAccount> entity);
    }
}
