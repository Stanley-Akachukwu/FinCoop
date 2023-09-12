using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
public class QueryFixedDepositAccountApplicationCommand : IRequest<CommandResult<IQueryable<FixedDepositAccountApplication>>>
{

}


