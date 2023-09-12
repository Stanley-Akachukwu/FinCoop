using AP.ChevronCoop.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules
{
public partial class SpecialDepositInterestScheduleConfiguration : BaseEntityConfiguration<SpecialDepositInterestSchedule, string>
{
    public override void Configure(EntityTypeBuilder<SpecialDepositInterestSchedule> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(SpecialDepositInterestSchedule), DbSchemaConstants.Deposits);

        entity.HasIndex(x => new { x.CronJobConfigId });

        entity.HasOne(e => e.CronJobConfig)
        .WithMany()
         .HasForeignKey(e => e.CronJobConfigId);

        entity.Property(l => l.ScheduleName)
          .IsRequired();

        entity.Property(l => l.StartDate)
          .IsRequired();

        entity.Property(l => l.EndDate)
          .IsRequired();


        entity.Property(l => l.ProcessedDate);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<SpecialDepositInterestSchedule> entity);
}

}


