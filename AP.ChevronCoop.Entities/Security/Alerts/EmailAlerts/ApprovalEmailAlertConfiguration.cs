using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Security.Alerts.EmailAlerts
{
 
	public partial class ApprovalEmailAlertConfiguration : BaseEntityConfiguration<ApprovalEmailAlert, string>
	{
		public override void Configure(EntityTypeBuilder<ApprovalEmailAlert> entity)
		{
			base.Configure(entity);

			entity.ToTable(nameof(ApprovalEmailAlert), DbSchemaConstants.Security);

			entity.Property(e => e.ApprovalId)
				.HasColumnType("nvarchar")
				.HasMaxLength(40)
			  .IsRequired(true);

			entity.Property(e => e.Description)
				  .HasColumnType("nvarchar")
				  .HasMaxLength(255)
				  .IsRequired(true);
			
			entity.Property(e => e.EmailBody)
			  .HasColumnType("nvarchar")
			  .HasMaxLength(800)
			  .IsRequired(true);
			
			entity.Property(e => e.EmailTitle)
				 .HasColumnType("nvarchar")
				 .HasMaxLength(80)
				 .IsRequired(true);
			
			OnConfigurePartial(entity);
		}

		partial void OnConfigurePartial(EntityTypeBuilder<ApprovalEmailAlert> entity);
	}
}
