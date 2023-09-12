using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
public class QueryFixedDepositAccountCommand : IRequest<CommandResult<IQueryable<FixedDepositAccount>>>
{

}


