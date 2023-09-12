using AP.ChevronCoop.Entities;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;

public class PayrollCronJobConfigViewModel : BaseViewModel
{
  public CronJobType CronJobType { get; set; }
  public string JobName { get; set; }
  public DateTime ProcessingDate { get; set; }
  public DateTime ProcessingEndDate { get; set; }


  public string? DeductionScheduleId { get; set; }
  public DateTime JobDate { get; set; }
  public CronJobStatus JobStatus { get; set; }
  public DateTime ComputationStartDate { get; set; }
  public DateTime ComputationEndDate { get; set; }
  public int RecordsProcessed { get; set; }
}