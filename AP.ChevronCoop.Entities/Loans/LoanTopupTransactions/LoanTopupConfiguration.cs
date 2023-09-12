using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.LoanTopupTransactions;

public partial class LoanTopupConfiguration : BaseEntityConfiguration<LoanTopup, string>
{
    public override void Configure(EntityTypeBuilder<LoanTopup> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanTopup), DbSchemaConstants.Loans);

        entity.Property(lt => lt.TopupAmount).IsRequired();

        entity.Property(lt => lt.OldPrincipalBalance)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lt => lt.NewPrincipalBalance)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lt => lt.OldInterestBalance)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lt => lt.NewInterestBalance)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lt => lt.TotalTopupCharges)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(lt => lt.TopupDate).IsRequired();
        entity.Property(lt => lt.CommencementDate).IsRequired();

        entity.Property(lpc => lpc.LoanAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(lt => lt.LoanAccount)
            .WithMany()
            .HasForeignKey(lt => lt.LoanAccountId)
            .IsRequired();

        entity.Property(lpc => lpc.SpecialDepositAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(lt => lt.SpecialDepositAccount)
            .WithMany()
            .HasForeignKey(lt => lt.SpecialDepositAccountId)
            .IsRequired(false);

        entity.Property(lpc => lpc.CustomerBankAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(lt => lt.CustomerBankAccount)
            .WithMany()
            .HasForeignKey(lt => lt.CustomerBankAccountId)
            .IsRequired(false);

        entity.Property(lpc => lpc.TransactionJournalId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(lt => lt.TransactionJournal)
            .WithMany()
            .HasForeignKey(lt => lt.TransactionJournalId)
            .IsRequired(false);

        entity.Property(lpc => lpc.ApprovalId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.Approval)
            .WithMany()
            .HasForeignKey(e => e.ApprovalId)
            .IsRequired(false);

        entity.Property(e => e.DestinationType).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanTopup> entity);
}