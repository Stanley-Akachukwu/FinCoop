using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;

public partial class MemberBeneficiaryConfiguration : BaseEntityConfiguration<MemberBeneficiary, string>
{
  public override void Configure(EntityTypeBuilder<MemberBeneficiary> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(MemberBeneficiary), DbSchemaConstants.Security);

    entity.Property(e => e.LastName)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired();
    
    entity.Property(e => e.FirstName)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired();
    
    entity.Property(e => e.Address)
      .HasColumnType("nvarchar")
      .HasMaxLength(512)
      .IsRequired();
    
    entity.Property(e => e.Phone)
      .HasColumnType("nvarchar")
      .HasMaxLength(32)
      .IsRequired();
    
    entity.Property(e => e.Email)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<MemberBeneficiary> entity);
}