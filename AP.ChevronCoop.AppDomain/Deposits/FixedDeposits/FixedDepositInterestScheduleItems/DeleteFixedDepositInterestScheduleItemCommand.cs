using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestScheduleItems;
public partial class DeleteFixedDepositInterestScheduleItemCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


