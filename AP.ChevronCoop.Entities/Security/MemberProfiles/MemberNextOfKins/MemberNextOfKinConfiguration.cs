using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;

public partial class MemberNextOfKinConfiguration : BaseEntityConfiguration<MemberNextOfKin, string>
{
  public override void Configure(EntityTypeBuilder<MemberNextOfKin> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(MemberNextOfKin), DbSchemaConstants.Security);

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
    
    entity.Property(e => e.Relationship)
      .HasColumnType("nvarchar")
      .HasMaxLength(128)
      .IsRequired();
    
    entity.Property(e => e.Email)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<MemberNextOfKin> entity);
}