
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestAdditions
{
    public class QuerySpecialDepositInterestAdditionCommand : IRequest<CommandResult<IQueryable<SpecialDepositInterestAddition>>>
    {

    }
}
 
