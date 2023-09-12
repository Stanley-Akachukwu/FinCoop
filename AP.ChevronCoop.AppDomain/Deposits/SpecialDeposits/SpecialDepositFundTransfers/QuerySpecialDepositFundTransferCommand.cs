using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositFundTransfers
{
    public class QuerySpecialDepositFundTransferCommand : IRequest<CommandResult<IQueryable<SpecialDepositFundTransfer>>>
    {

    }
}
 
 
