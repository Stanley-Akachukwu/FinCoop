using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions
{
    public class QuerySpecialDepositCashAdditionCommand : IRequest<CommandResult<IQueryable<SpecialDepositCashAddition>>>
    {

    }
}
 
