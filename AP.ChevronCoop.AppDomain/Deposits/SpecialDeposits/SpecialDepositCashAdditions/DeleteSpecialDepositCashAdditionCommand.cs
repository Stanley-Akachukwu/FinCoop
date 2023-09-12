using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions
{
    public partial class DeleteSpecialDepositCashAdditionCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }
}
 
