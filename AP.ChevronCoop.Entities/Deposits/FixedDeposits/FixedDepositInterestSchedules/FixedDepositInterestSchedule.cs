using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestSchedules
{
    public class FixedDepositInterestSchedule : BaseEntity<string>
    {
        public FixedDepositInterestSchedule()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string CronJobConfigId { get; set; }
        public PayrollCronJobConfig CronJobConfig { get; set; }

        public string ScheduleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 

        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }

        public virtual List<FixedDepositInterestScheduleItem> ScheduleItems { get; set; }

        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }
    }
}
