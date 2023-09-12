using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
public class QueryFixedDepositChangeInMaturityCommand : IRequest<CommandResult<IQueryable<FixedDepositChangeInMaturity>>>
{

}


