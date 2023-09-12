using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanProducts;

public partial class LoanProductConfiguration : BaseEntityConfiguration<LoanProduct, string>
{
    public override void Configure(EntityTypeBuilder<LoanProduct> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(LoanProduct), DbSchemaConstants.Loans);

        entity.HasIndex(x => new { x.Name }).IsUnique();
        entity.HasIndex(x => new { x.Name, x.Code }).IsUnique();

        entity.Property(e => e.Code)
          .HasColumnType("nvarchar")
          .HasMaxLength(32)
          .IsRequired();

        entity.Property(e => e.Name)
          .HasColumnType("nvarchar")
          .HasMaxLength(128)
          .IsRequired();

        entity.Property(e => e.ShortName)
          .HasColumnType("nvarchar")
          .HasMaxLength(128)
          .IsRequired();

        entity.Property(e => e.MinimumAge)
          .HasDefaultValue(0)
          .IsRequired(false);

        entity.Property(e => e.MaximumAge)
          .HasDefaultValue(0)
          .IsRequired(false);

        entity.Property(lpc => lpc.DefaultCurrencyId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.DefaultCurrency)
          .WithMany()
          .HasForeignKey(e => e.DefaultCurrencyId)
          .IsRequired(false);

        entity.Property(lpc => lpc.BankDepositAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.BankDepositAccount)
          .WithMany()
          .HasForeignKey(e => e.BankDepositAccountId)
          .IsRequired(false);

        entity.Property(lpc => lpc.DisbursementAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.DisbursementAccount)
            .WithMany()
            .HasForeignKey(e => e.DisbursementAccountId)
            .IsRequired(false);

        entity.Property(lpc => lpc.PrincipalAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.PrincipalAccount)
          .WithMany()
          .HasForeignKey(e => e.PrincipalAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.PrincipalLossAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.PrincipalLossAccount)
          .WithMany()
          .HasForeignKey(e => e.PrincipalLossAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.InterestIncomeAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.InterestIncomeAccount)
          .WithMany()
          .HasForeignKey(e => e.InterestIncomeAccountId)
          .IsRequired();
        
        entity.Property(lpc => lpc.UnearnedInterestAccountId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(e => e.UnearnedInterestAccount)
          .WithMany()
          .HasForeignKey(e => e.UnearnedInterestAccountId)
          .IsRequired();

        
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
        
        entity.Property(lpc => lpc.InterestLossAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.InterestLossAccount)
          .WithMany()
          .HasForeignKey(e => e.InterestLossAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.PenalInterestReceivableAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.PenalInterestReceivableAccount)
          .WithMany()
          .HasForeignKey(e => e.PenalInterestReceivableAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.ChargesIncomeAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.ChargesIncomeAccount)
          .WithMany()
          .HasForeignKey(e => e.ChargesIncomeAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.ChargesAccrualAccountId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.ChargesAccrualAccount)
          .WithMany()
          .HasForeignKey(e => e.ChargesAccrualAccountId)
          .IsRequired();

        entity.Property(lpc => lpc.ApprovalId)
            .HasColumnType("nvarchar")
            .HasMaxLength(40);

        entity.HasOne(e => e.Approval)
          .WithMany()
          .HasForeignKey(e => e.ApprovalId)
          .IsRequired(false);

        entity.Property(lpc => lpc.ApprovalWorkflowId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);
        entity.HasOne(e => e.ApprovalWorkflow)
          .WithMany()
          .HasForeignKey(e => e.ApprovalWorkflowId)
          .IsRequired();

        entity.Property(e => e.PrincipalMultiple)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(e => e.PrincipalMinLimit)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(e => e.PrincipalMaxLimit)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(e => e.InterestRate)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(e => e.BenefitCode)
          .HasColumnType("nvarchar")
          .HasMaxLength(32)
          .IsRequired(false);

        entity.Property(e => e.GuarantorAmountLimit)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();

        entity.Property(e => e.QualificationMinBalancePercentage)
          .HasPrecision(18, 2)
          .HasDefaultValue(0.00)
          .IsRequired();


        entity.Property(e => e.DaysInYear)
          .HasDefaultValue(365);
        //.IsRequired();

        entity.Property(e => e.TenureUnit).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.RepaymentPeriod).HasMaxLength(100).HasConversion<string>()
            .IsRequired(true).HasDefaultValue(Tenure.MONTHLY);
        entity.Property(e => e.InterestMethod).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.InterestCalculationMethod).HasMaxLength(100).HasConversion<string>()
            .IsRequired(true).HasDefaultValue(InterestCalculationMethod.FLAT_RATE);
        entity.Property(e => e.Status).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.LoanProductType).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.AllowedOffsetType).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.OffsetPeriodUnit).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.WaitingPeriodUnit).HasMaxLength(100).HasConversion<string>();
        entity.Property(e => e.QualificationTargetProduct).HasMaxLength(100).HasConversion<string>();


        // entity.OwnsMany(e => e.MemberTypeIdList, builder => { builder.ToJson(); });
        // entity.OwnsMany(e => e.SavingsOffSetProductIdList, builder => { builder.ToJson(); });

        entity.Property(e => e.MemberTypeIdList)
          .HasConversion(
            v => JsonSerializer.Serialize(v, typeof(List<String>), JsonSerializerOptions.Default),
            v => JsonSerializer.Deserialize<List<String>>(v, JsonSerializerOptions.Default)
          );

        entity.Property(e => e.SavingsOffSetProductIdList)
          .HasConversion(
            v => JsonSerializer.Serialize(v, typeof(List<String>), JsonSerializerOptions.Default),
            v => JsonSerializer.Deserialize<List<String>>(v, JsonSerializerOptions.Default)
          );

        //data.MemberTypeIdList.Add("234");
        //data.MemberTypeIdList.Remove("234");
        ////.Where(p=>data.MemberTypeIdList.Contains("234"));

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LoanProduct> entity);
}
