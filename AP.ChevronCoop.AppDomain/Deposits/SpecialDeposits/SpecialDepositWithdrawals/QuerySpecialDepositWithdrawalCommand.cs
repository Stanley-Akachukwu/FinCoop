
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals
{
    public class QuerySpecialDepositWithdrawalCommand : IRequest<CommandResult<IQueryable<SpecialDepositWithdrawal>>>
    {

    }

}

