using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestSchedules;
public partial class DeleteFixedDepositInterestScheduleCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


