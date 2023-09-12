using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanOffsets;

public class DeleteLoanOffsetCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}