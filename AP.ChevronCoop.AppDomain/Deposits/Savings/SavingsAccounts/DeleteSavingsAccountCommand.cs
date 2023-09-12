using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
public partial class DeleteSavingsAccountCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


