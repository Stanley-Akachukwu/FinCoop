using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
public partial class DeleteSavingsAccountApplicationCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}


