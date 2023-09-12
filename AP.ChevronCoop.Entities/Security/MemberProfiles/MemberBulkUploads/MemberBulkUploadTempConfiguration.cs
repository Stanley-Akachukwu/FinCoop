using AP.ChevronCoop.Entities.Security.AuditTrails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads
{
     

    public partial class MemberBulkUploadTempConfiguration : BaseEntityConfiguration<MemberBulkUploadTemp, string>
    {
        public override void Configure(EntityTypeBuilder<MemberBulkUploadTemp> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(MemberBulkUploadTemp), DbSchemaConstants.Security);

       
            entity.Property(e => e.MembershipNumber)
              .HasColumnType("nvarchar")
              .HasMaxLength(128);

            entity.Property(e => e.LastName)
              .HasColumnName("LastName")
              .HasColumnType("nvarchar")
              .HasMaxLength(128);


            entity.Property(e => e.FirstName)
              .HasColumnName("FirstName")
              .HasColumnType("nvarchar")
              .HasMaxLength(128);

            entity.Property(e => e.Status).HasMaxLength(32).HasConversion<string>();
            entity.Property(e => e.Gender).HasMaxLength(32).HasConversion<string>();
           //  entity.Property(e => e.MemberType).HasMaxLength(100).HasConversion<string>();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<MemberBulkUploadTemp> entity);
    }
}
