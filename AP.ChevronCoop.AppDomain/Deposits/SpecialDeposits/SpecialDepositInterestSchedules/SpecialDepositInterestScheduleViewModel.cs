using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestSchedules
{
    public partial class SpecialDepositInterestScheduleViewModel : BaseViewModel
    {
        public string CronJobConfigId { get; set; }
        public string ScheduleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsProcessed { get; set; }

        public DateTime? ProcessedDate { get; set; }

    }

}

