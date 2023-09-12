using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestSchedules;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public partial class FixedDepositInterestScheduleConfiguration : BaseEntityConfiguration<FixedDepositInterestSchedule, string>
{
    public override void Configure(EntityTypeBuilder<FixedDepositInterestSchedule> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(FixedDepositInterestSchedule), DbSchemaConstants.Deposits);

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

    partial void OnConfigurePartial(EntityTypeBuilder<FixedDepositInterestSchedule> entity);
}
