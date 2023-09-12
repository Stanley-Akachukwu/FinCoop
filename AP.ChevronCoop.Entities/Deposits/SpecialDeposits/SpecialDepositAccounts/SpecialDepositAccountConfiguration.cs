using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;

public partial class SpecialDepositAccountConfiguration : BaseEntityConfiguration<SpecialDepositAccount, string>
{
    public override void Configure(EntityTypeBuilder<SpecialDepositAccount> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(SpecialDepositAccount), DbSchemaConstants.Deposits);

        entity.HasIndex(x => new { x.DepositProductId });
        entity.HasIndex(x => new { x.CustomerId });
        entity.HasIndex(x => new { x.ApplicationId });
        entity.HasIndex(x => new { x.AccountNo }).IsUnique();


        entity.Property(e => e.AccountNo)
         .HasColumnType("nvarchar")
         .HasMaxLength(50);

        entity.HasOne(e => e.Application)
          .WithMany()
          .HasForeignKey(e => e.ApplicationId);


        entity.HasOne(e => e.DepositProduct)
          .WithMany()
          .HasForeignKey(e => e.DepositProductId);

        entity.HasOne(e => e.Customer)
          .WithMany()
          .HasForeignKey(e => e.CustomerId);

        entity.HasOne(e => e.InterestPayoutAccount)
          .WithMany()
          .HasForeignKey(e => e.InterestPayoutAccountId);

        entity.Property(e => e.ChargesAccruedAccountId).HasColumnType("nvarchar").HasMaxLength(40); 
        entity.HasOne(e => e.ChargesAccruedAccount)
        .WithMany()
        .HasForeignKey(e => e.ChargesAccruedAccountId)
        .IsRequired();


        entity.Property(e => e.ChargesIncomeAccountId).HasColumnType("nvarchar").HasMaxLength(40);
        entity.HasOne(e => e.ChargesIncomeAccount)
        .WithMany()
        .HasForeignKey(e => e.ChargesIncomeAccountId)
        .IsRequired();

        entity.Property(lpc => lpc.ChargesWaivedAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.ChargesWaivedAccount)
          .WithMany()
          .HasForeignKey(e => e.ChargesWaivedAccountId)
          .IsRequired();

        entity.HasOne(e => e.DepositAccount)
          .WithMany()
          .HasForeignKey(e => e.DepositAccountId);

        entity.HasOne(e => e.DepositAccount)
          .WithMany()
          .HasForeignKey(e => e.DepositAccountId);

        entity.HasOne(e => e.InterestEarnedAccount)
          .WithMany()
          .HasForeignKey(e => e.InterestEarnedAccountId);

        entity.HasOne(e => e.ClosedByUser)
          .WithMany()
          .HasForeignKey(e => e.ClosedByUserId)
          .IsRequired(false);


        entity.Property(e => e.FundingAmount)
          .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();

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

        entity.Property(e => e.InterestRate)
          .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();


        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<SpecialDepositAccount> entity);
}