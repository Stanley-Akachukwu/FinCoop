using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;

public partial class MemberBulkUploadSessionConfiguration : BaseEntityConfiguration<MemberBulkUploadSession, string>
{
  public override void Configure(EntityTypeBuilder<MemberBulkUploadSession> entity)
  {
    base.Configure(entity);

    entity.ToTable(nameof(MemberBulkUploadSession), DbSchemaConstants.Security);

    
    
    OnConfigurePartial(entity);
  }

  partial void OnConfigurePartial(EntityTypeBuilder<MemberBulkUploadSession> entity);
}