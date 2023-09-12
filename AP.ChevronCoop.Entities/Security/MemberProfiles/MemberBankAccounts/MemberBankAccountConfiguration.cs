using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBankAccounts;

public partial class MemberBankAccountConfiguration : BaseEntityConfiguration<MemberBankAccount, string>
{
    public override void Configure(EntityTypeBuilder<MemberBankAccount> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(MemberBankAccount), DbSchemaConstants.Security);

        entity.Property(e => e.AccountNumber)
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();

        // entity.Property(e => e.SortCode)
        //   .HasColumnType("nvarchar")
        //   .HasMaxLength(50);

        entity.Property(e => e.AccountName)
          .HasColumnType("nvarchar")
          .HasMaxLength(128)
          .IsRequired();

        entity.Property(e => e.BVN)
          .HasColumnType("nvarchar")
          .HasMaxLength(32)
          .IsRequired(false);

        entity.Property(e => e.Branch)
          .HasColumnType("nvarchar")
          .HasMaxLength(64);

        entity.Property(e => e.ProfileId)
          .HasColumnType("nvarchar");
        
        entity.HasOne(e => e.MemberProfile)
          .WithMany()
          .HasForeignKey(e => e.ProfileId);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<MemberBankAccount> entity);
}