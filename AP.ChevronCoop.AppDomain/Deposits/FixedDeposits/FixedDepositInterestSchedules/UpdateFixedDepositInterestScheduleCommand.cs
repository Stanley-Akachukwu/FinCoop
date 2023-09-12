using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestSchedules;
public partial class UpdateFixedDepositInterestScheduleCommand : UpdateCommand, IRequest<CommandResult<FixedDepositInterestScheduleViewModel>>
{
    

    public string CronJobConfigId { get; set; }

    public string ScheduleName { get; set; }

    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
 
    public bool IsProcessed { get; set; }

    public DateTime ProcessedDate { get; set; }

}


