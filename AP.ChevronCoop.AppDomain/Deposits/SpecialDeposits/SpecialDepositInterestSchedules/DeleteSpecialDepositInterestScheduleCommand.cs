using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestSchedules
{
    public partial class DeleteSpecialDepositInterestScheduleCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }
}
 
 
