using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems
{
    public partial class DeleteSpecialDepositInterestScheduleItemCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }
}
 
