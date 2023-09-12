using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestAdditions;
public class QueryFixedDepositInterestAdditionCommand : IRequest<CommandResult<IQueryable<FixedDepositInterestAddition>>>
{

}


