using AP.ChevronCoop.Commons;
using MediatR;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccounts
{
    public class QuerySpecialDepositAccountCommand : IRequest<CommandResult<IQueryable<SpecialDepositAccount>>>
    {

    }
}


