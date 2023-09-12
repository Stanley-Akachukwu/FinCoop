using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
public class QuerySavingsCashAdditionCommand : IRequest<CommandResult<IQueryable<SavingsCashAddition>>>
{

}


