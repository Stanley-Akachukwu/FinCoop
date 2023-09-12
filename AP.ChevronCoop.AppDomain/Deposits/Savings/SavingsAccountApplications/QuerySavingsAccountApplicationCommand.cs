using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
public class QuerySavingsAccountApplicationCommand : IRequest<CommandResult<IQueryable<SavingsAccountApplication>>>
{

}


