using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositFundTransfers
{
    public partial class DeleteSpecialDepositFundTransferCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }
}
 
