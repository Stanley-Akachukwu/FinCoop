using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
public class QueryFixedDepositLiquidationCommand : IRequest<CommandResult<IQueryable<FixedDepositLiquidation>>>
{

}


