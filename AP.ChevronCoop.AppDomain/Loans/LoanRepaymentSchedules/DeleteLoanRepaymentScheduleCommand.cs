using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanRepaymentSchedules;

public class DeleteLoanRepaymentScheduleCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}