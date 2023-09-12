using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;

public partial class CompanyBankAccountConfiguration : BaseEntityConfiguration<CompanyBankAccount, string>
{
  public override void Configure(EntityTypeBuilder<CompanyBankAccount> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(CompanyBankAccount), DbSchemaConstants.Accounting);

    entity.HasIndex(x => new { x.BankId });
    entity.HasIndex(x => new { x.BankId, x.LedgerAccountId }).IsUnique();
            
    entity.Property(b => b.BranchName)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);
    
    entity.Property(b => b.BranchAddress)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

    entity.Property(b => b.AccountName)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired();
    
    entity.Property(b => b.AccountNumber)
      .HasColumnType("nvarchar")
      .HasMaxLength(32)
      .IsRequired();
    
    entity.Property(b => b.BVN)
      .HasColumnType("nvarchar")
      .HasMaxLength(32);

    entity.Property(b => b.BankId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40)
      .IsRequired();
            
    entity.HasOne(b => b.Bank)
      .WithMany()
      .HasForeignKey(b => b.BankId)
      .OnDelete(DeleteBehavior.Restrict)
      .IsRequired();
    
    entity.Property(b => b.LedgerAccountId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40)
      .IsRequired();
    
    entity.HasOne(b => b.LedgerAccount)
      .WithMany()
      .HasForeignKey(b => b.LedgerAccountId)
      .OnDelete(DeleteBehavior.Restrict)
      .IsRequired();
            
            
    entity.Property(b => b.CurrencyId)
      .HasColumnType("nvarchar")
      .HasMaxLength(40)
      .IsRequired();
    
    entity.HasOne(b => b.Currency)
      .WithMany()
      .HasForeignKey(b => b.CurrencyId)
      .OnDelete(DeleteBehavior.Restrict)
      .IsRequired();


    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<CompanyBankAccount> entity);
}