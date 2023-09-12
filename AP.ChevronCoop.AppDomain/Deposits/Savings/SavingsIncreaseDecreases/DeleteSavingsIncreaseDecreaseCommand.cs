using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
public partial class DeleteSavingsIncreaseDecreaseCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


