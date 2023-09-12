using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;

public class DeleteLoanApplicationScheduleCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}