using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.AuditTrails;

public partial class AuditTrailConfiguration: BaseEntityConfiguration<AuditTrail, string>
{
  public override void Configure(EntityTypeBuilder<AuditTrail> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(AuditTrail), DbSchemaConstants.Security);

    entity.HasIndex(x => new { x.ApplicationUserId }).IsUnique(false);
    

    entity.Property(e => e.Module)
      .HasColumnType("nvarchar")
      .HasMaxLength(128);

   
    
    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<AuditTrail> entity);
}