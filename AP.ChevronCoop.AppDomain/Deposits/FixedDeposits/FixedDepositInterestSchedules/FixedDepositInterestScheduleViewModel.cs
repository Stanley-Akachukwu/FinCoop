namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestSchedules;
public partial class FixedDepositInterestScheduleViewModel : BaseViewModel
{

    public string CronJobConfigId { get; set; }
    public string ScheduleName { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool IsProcessed { get; set; }

    public DateTime ProcessedDate { get; set; }

}


