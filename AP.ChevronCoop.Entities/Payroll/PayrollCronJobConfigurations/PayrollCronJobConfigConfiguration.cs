using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations
{

    public partial class PayrollCronJobConfigConfiguration : BaseEntityConfiguration<PayrollCronJobConfig, string>
    {
        public override void Configure(EntityTypeBuilder<PayrollCronJobConfig> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(PayrollCronJobConfig), DbSchemaConstants.Payroll);

            entity.Property(e => e.CronJobType).HasMaxLength(100).HasConversion<string>();
            entity.Property(e => e.JobStatus).HasMaxLength(100).HasConversion<string>();



            entity.Property(pd => pd.DeductionScheduleId).HasMaxLength(40).IsRequired(false);

            entity.HasOne(pd => pd.DeductionSchedule)
              .WithMany(pd => pd.PayrollCronJobs)
              .HasForeignKey(pd => pd.DeductionScheduleId)
              .IsRequired(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PayrollCronJobConfig> entity);
    }

}
