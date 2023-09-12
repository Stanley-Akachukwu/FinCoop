using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
public partial class DeleteFixedDepositAccountCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


