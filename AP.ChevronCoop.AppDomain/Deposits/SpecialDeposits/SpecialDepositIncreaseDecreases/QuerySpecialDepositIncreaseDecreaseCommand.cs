using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
public class QuerySpecialDepositIncreaseDecreaseCommand : IRequest<CommandResult<IQueryable<SpecialDepositIncreaseDecrease>>>
{

}


