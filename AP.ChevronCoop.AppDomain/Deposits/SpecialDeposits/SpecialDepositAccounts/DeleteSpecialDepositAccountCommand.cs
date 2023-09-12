using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccounts
{
    public partial class DeleteSpecialDepositAccountCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }
}


