using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;

public class CreatePayrollCronJobConfigCommand : IRequest<CommandResult<PayrollCronJobConfigViewModel>>
{
  public CronJobType CronJobType { get; set; }
  public string JobName { get; set; }
  public DateTime ProcessingDate { get; set; }
  public string CreatedByUserId { get; set; }
  public DateTime ProcessingEndDate { get; set; }
  public CronJobStatus JobStatus { get; set; } = CronJobStatus.PENDING;
  public string? DeductionScheduleId { get; set; }
  public DateTime JobDate { get; set; }
  public DateTime ComputationStartDate { get; set; }
  public DateTime ComputationEndDate { get; set; }
  //public int RecordsProcessed { get; set; }



   
}