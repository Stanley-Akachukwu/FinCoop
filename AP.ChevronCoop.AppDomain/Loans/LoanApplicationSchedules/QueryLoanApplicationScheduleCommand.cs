using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanApplicationSchedules;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;

public class QueryLoanApplicationScheduleCommand : IRequest<CommandResult<IQueryable<LoanApplicationSchedule>>>
{
}