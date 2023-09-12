
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestAdditions
{
    public partial class DeleteSpecialDepositInterestAdditionCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }
}
 
 
