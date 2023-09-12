using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
public class QuerySavingsAccountCommand : IRequest<CommandResult<IQueryable<SavingsAccount>>>
{

}


