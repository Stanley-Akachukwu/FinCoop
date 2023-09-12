using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;

public partial class SpecialDepositWithdrawalConfiguration : BaseEntityConfiguration<SpecialDepositWithdrawal, string>
{
    public override void Configure(EntityTypeBuilder<SpecialDepositWithdrawal> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(SpecialDepositWithdrawal), DbSchemaConstants.Deposits);


        entity.Property(e => e.Amount)
          .HasPrecision(18, 2)
              .HasDefaultValue(0.00)
              .IsRequired();


        entity.HasOne(e => e.SpecialDepositSourceAccount)
        .WithMany()
        .HasForeignKey(e => e.SpecialDepositSourceAccountId);

        //entity.HasOne(e => e.CustomerDestinationBankAccount)
        //.WithMany()
        //.HasForeignKey(e => e.CustomerDestinationBankAccountId);
        entity.HasOne(e => e.TransactionJournal)
       .WithMany()
       .HasForeignKey(e => e.TransactionJournalId);

        entity.Property(e => e.WithdrawalDestinationType).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();

        entity.Property(e => e.ApprovalId).HasColumnType("nvarchar").HasMaxLength(40);
        entity.HasOne(e => e.Approval)
            .WithMany()
            .HasForeignKey(e => e.ApprovalId);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<SpecialDepositWithdrawal> entity);
}
