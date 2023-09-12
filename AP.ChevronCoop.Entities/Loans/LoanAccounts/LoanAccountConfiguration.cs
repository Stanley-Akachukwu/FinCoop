using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanAccounts;

public partial class LoanAccountConfiguration : BaseEntityConfiguration<LoanAccount, string>
{
    public override void Configure(EntityTypeBuilder<LoanAccount> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanAccount), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.LoanApplicationId });
        entity.HasIndex(x => new { x.AccountNo }).IsUnique();

        entity.Property(e => e.AccountNo)
          .HasColumnType("nvarchar")
          .HasMaxLength(64)
          .IsRequired();

        entity.Property(lpc => lpc.LoanApplicationId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.LoanApplication)
          .WithMany()
          .HasForeignKey(e => e.LoanApplicationId)
          .IsRequired();

        entity.Property(lpc => lpc.LoanTopupId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.LoanTopup)
          .WithMany()
          .HasForeignKey(e => e.LoanTopupId)
          .IsRequired(false);

        entity.Property(lpc => lpc.CustomerId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.Customer)
          .WithMany()
          .HasForeignKey(e => e.CustomerId)
          .IsRequired();

        entity.Property(lpc => lpc.PrincipalBalanceAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.PrincipalBalanceAccount)
          .WithMany()
          .HasForeignKey(e => e.PrincipalBalanceAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.PrincipalLossAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.PrincipalLossAccount)
          .WithMany()
          .HasForeignKey(e => e.PrincipalLossAccountId)
          .IsRequired();

        //entity.Property(lpc => lpc.EarnedInterestAccountId)
        //  .HasColumnType("nvarchar")
        //  .HasMaxLength(40);

        //entity.HasOne(e => e.EarnedInterestAccount)
        //  .WithMany()
        //  .HasForeignKey(e => e.EarnedInterestAccountId)
        //  .IsRequired();

        entity.Property(e => e.EarnedInterestAccountId).HasColumnType("nvarchar").HasMaxLength(40); ;
        entity.HasOne(e => e.EarnedInterestAccount)
          .WithMany()
          .HasForeignKey(e => e.EarnedInterestAccountId)
          .IsRequired();

        entity.Property(e => e.InterestPayoutAccountId).HasColumnType("nvarchar").HasMaxLength(40); ;
        entity.HasOne(e => e.InterestPayoutAccount)
          .WithMany()
          .HasForeignKey(e => e.InterestPayoutAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.InterestLossAccountId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40);

        entity.HasOne(e => e.InterestLossAccount)
          .WithMany()
          .HasForeignKey(e => e.InterestLossAccountId)
          .IsRequired();

        // entity.Property(lpc => lpc.ChargesPayableAccountId)
        //   .HasColumnType("nvarchar")
        //   .HasMaxLength(40);
        //
        // entity.HasOne(e => e.ChargesPayableAccount)
        //   .WithMany()
        //   .HasForeignKey(e => e.ChargesPayableAccountId)
        //   .IsRequired();

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

        entity.Property(lpc => lpc.UnearnedInterestAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.UnearnedInterestAccount)
          .WithMany()
          .HasForeignKey(e => e.UnearnedInterestAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.SpecialDepositAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.SpecialDepositAccount)
          .WithMany()
          .HasForeignKey(e => e.SpecialDepositAccountId)
          .IsRequired(false);

        entity.Property(lpc => lpc.InterestBalanceAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.InterestBalanceAccount)
          .WithMany()
          .HasForeignKey(e => e.InterestBalanceAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.DestinationAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.DestinationAccount)
          .WithMany()
          .HasForeignKey(e => e.DestinationAccountId)
          .IsRequired(false);

        entity.Property(lpc => lpc.ChargesWaivedAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.ChargesWaivedAccount)
          .WithMany()
          .HasForeignKey(e => e.ChargesWaivedAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.InterestWaivedAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.InterestWaivedAccount)
          .WithMany()
          .HasForeignKey(e => e.InterestWaivedAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.ClosedByUserId)
          .HasColumnType("nvarchar")
          .HasMaxLength(450);

        entity.HasOne(e => e.ClosedByUser)
          .WithMany()
          .HasForeignKey(e => e.ClosedByUserId)
          .IsRequired(false);

        entity.Property(e => e.Principal)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.HasOne(e => e.RootParentAccount)
            .WithMany()
            .HasForeignKey(e => e.RootParentAccountId);

        entity.HasOne(e => e.ParentAccount)
          .WithMany()
          .HasForeignKey(e => e.ParentAccountId);

        entity.Property(e => e.TenureUnit).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.LoanCreationType).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanAccount> entity);
}