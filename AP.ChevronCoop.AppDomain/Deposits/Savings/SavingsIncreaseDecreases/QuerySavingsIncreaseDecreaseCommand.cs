using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
public class QuerySavingsIncreaseDecreaseCommand : IRequest<CommandResult<IQueryable<SavingsIncreaseDecrease>>>
{

}


