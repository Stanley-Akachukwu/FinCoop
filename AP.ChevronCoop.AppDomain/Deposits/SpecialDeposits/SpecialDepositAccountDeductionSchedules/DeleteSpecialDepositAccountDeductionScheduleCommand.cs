using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules
{
    public partial class DeleteSpecialDepositAccountDeductionScheduleCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }
}
 
