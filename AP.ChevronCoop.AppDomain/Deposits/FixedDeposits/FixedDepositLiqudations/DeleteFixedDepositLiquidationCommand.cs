using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
public partial class DeleteFixedDepositLiquidationCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


