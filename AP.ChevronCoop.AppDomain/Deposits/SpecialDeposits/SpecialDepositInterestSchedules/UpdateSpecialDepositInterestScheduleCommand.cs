using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestSchedules
{
    public partial class UpdateSpecialDepositInterestScheduleCommand : UpdateCommand, IRequest<CommandResult<SpecialDepositInterestScheduleViewModel>>
    {
        public string CronJobConfigId { get; set; }
        public string ScheduleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }

    }

}

