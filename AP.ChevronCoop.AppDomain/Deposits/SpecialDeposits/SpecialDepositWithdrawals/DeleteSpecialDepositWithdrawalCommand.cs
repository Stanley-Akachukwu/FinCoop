using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals
{
    public partial class DeleteSpecialDepositWithdrawalCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }

}

