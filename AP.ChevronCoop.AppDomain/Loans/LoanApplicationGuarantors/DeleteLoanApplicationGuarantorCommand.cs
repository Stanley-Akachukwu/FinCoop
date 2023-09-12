using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public class DeleteLoanApplicationGuarantorCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}