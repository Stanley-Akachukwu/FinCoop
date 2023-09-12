using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
public partial class UpdatePayrollCronJobConfigCommand : IRequest<CommandResult<PayrollCronJobConfigViewModel>>
{
    public string Id { get; set; }
    public CronJobType CronJobType { get; set; }
    public string JobName { get; set; }
    public DateTime ProcessingDate { get; set; }
    public DateTime ProcessingEndDate { get; set; }
    public string UpdatedByUserId { get; set; }
    public CronJobStatus JobStatus { get; set; } = CronJobStatus.PENDING;
    public string? DeductionScheduleId { get; set; }
    public DateTime JobDate { get; set; }
    public DateTime ComputationStartDate { get; set; }
    public DateTime ComputationEndDate { get; set; }
   // public int RecordsProcessed { get; set; }
}


