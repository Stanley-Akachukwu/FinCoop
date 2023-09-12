using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
public partial class DeleteSpecialDepositIncreaseDecreaseCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


