using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestAdditions;
public partial class DeleteFixedDepositInterestAdditionCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


