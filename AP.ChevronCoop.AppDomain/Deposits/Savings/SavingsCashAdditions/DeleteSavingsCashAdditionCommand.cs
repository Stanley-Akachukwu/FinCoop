using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
public partial class DeleteSavingsCashAdditionCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


