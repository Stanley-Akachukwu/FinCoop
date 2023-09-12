using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Deposits.Products.DepositProducts
{
    public partial class DepositProductConfiguration : BaseEntityConfiguration<DepositProduct, string>
    {
        public override void Configure(EntityTypeBuilder<DepositProduct> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(DepositProduct), DbSchemaConstants.Deposits);

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
              .IsRequired();

            entity.Property(e => e.MaximumAge)
              .IsRequired();
            
            
            entity.Property(e => e.ApprovalWorkflowId);
            entity.HasOne(e => e.ApprovalWorkflow)
             .WithMany()
             .HasForeignKey(e => e.ApprovalWorkflowId);
            
            
            entity.Property(e => e.ApprovalId);
            entity.HasOne(e => e.Approval)
              .WithMany()
              .HasForeignKey(e => e.ApprovalId);


            entity.Property(c => c.PublicationType).HasMaxLength(100).IsRequired().HasDefaultValue(PublicationType.ALL).HasConversion<string>();
            entity.Property(c => c.Tenure).HasMaxLength(100).IsRequired().HasDefaultValue(Tenure.NONE).HasConversion<string>();
            entity.Property(c => c.ProductType).HasMaxLength(100).IsRequired().HasConversion<string>();

            entity.Property(c => c.Status).HasMaxLength(100).IsRequired().HasDefaultValue(ProductStatus.PENDING_APPROVAL).HasConversion<string>();

            entity.Property(e => e.TenureValue).HasDefaultValue(0).IsRequired();

            entity.Property(e => e.DefaultCurrencyId)
             .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired(true);

            entity.HasOne(e => e.DefaultCurrency)
              .WithMany()
              .HasForeignKey(e => e.DefaultCurrencyId)
              .IsRequired();

            entity.Property(e => e.BankDepositAccountId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired(false);

            entity.HasOne(e => e.BankDepositAccount)
              .WithMany()
              .HasForeignKey(e => e.BankDepositAccountId)
              .IsRequired();

            entity.Property(e => e.ProductDepositAccountId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired(true);

            entity.HasOne(e => e.ProductDepositAccount)
              .WithMany()
              .HasForeignKey(e => e.ProductDepositAccountId);



            entity.Property(e => e.ChargesIncomeAccountId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired();

            entity.HasOne(e => e.ChargesIncomeAccount)
              .WithMany()
              .HasForeignKey(e => e.ChargesIncomeAccountId);


            entity.Property(lpc => lpc.ChargesWaivedAccountId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40);

            entity.HasOne(e => e.ChargesWaivedAccount)
              .WithMany()
              .HasForeignKey(e => e.ChargesWaivedAccountId)
              .IsRequired();

            entity.Property(e => e.ChargesAccrualAccountId)
            .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired();

            entity.HasOne(e => e.ChargesAccrualAccount)
              .WithMany()
              .HasForeignKey(e => e.ChargesAccrualAccountId)
              .IsRequired();

            entity.Property(e => e.InterestPayableAccountId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired();

            entity.HasOne(e => e.InterestPayableAccount)
              .WithMany()
              .HasForeignKey(e => e.InterestPayableAccountId)
              .IsRequired();

            entity.Property(e => e.InterestPayoutAccountId)
              .HasColumnType("nvarchar")
              .HasMaxLength(40)
              .IsRequired();

            entity.HasOne(e => e.InterestPayoutAccount)
              .WithMany()
              .HasForeignKey(e => e.InterestPayoutAccountId)
              .IsRequired();


            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DepositProduct> entity);
    }
}
