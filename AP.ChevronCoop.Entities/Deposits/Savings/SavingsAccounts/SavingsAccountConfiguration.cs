using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;

public partial class SavingsAccountConfiguration : BaseEntityConfiguration<SavingsAccount, string>
{
    public override void Configure(EntityTypeBuilder<SavingsAccount> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(SavingsAccount), DbSchemaConstants.Deposits);

        entity.HasIndex(x => new { x.DepositProductId });
        entity.HasIndex(x => new { x.CustomerId });
        entity.HasIndex(x => new { x.ApplicationId });
        entity.HasIndex(x => new { x.AccountNo }).IsUnique();

        entity.Property(e => e.AccountNo)
          .HasColumnType("nvarchar")
         .HasMaxLength(50)
         .IsRequired();


        entity.HasOne(e => e.Application)
          .WithMany()
          .HasForeignKey(e => e.ApplicationId);

        entity.HasOne(e => e.DepositProduct)
          .WithMany()
          .HasForeignKey(e => e.DepositProductId);

        entity.HasOne(e => e.Customer)
          .WithMany()
          .HasForeignKey(e => e.CustomerId);

        entity.HasOne(e => e.ChargesPayableAccount)
          .WithMany()
          .HasForeignKey(e => e.ChargesPayableAccountId);

        entity.HasOne(e => e.LedgerDepositAccount)
          .WithMany()
          .HasForeignKey(e => e.LedgerDepositAccountId);

        entity.HasOne(e => e.LedgerDepositAccount)
          .WithMany()
          .HasForeignKey(e => e.LedgerDepositAccountId);


        entity.Property(lpc => lpc.ChargesWaivedAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.ChargesWaivedAccount)
          .WithMany()
          .HasForeignKey(e => e.ChargesWaivedAccountId)
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


        entity.HasOne(e => e.ClosedByUser)
          .WithMany()
          .HasForeignKey(e => e.ClosedByUserId)
          .IsRequired(false);


        entity.Property(e => e.PayrollAmount)
          .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();


        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<SavingsAccount> entity);
}