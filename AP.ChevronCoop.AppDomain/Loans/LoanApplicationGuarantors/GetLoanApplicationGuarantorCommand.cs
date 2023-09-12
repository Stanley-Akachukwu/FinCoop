using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public record GetLoanApplicationGuarantorCommand
  (string LoanApplicationGuarantorId) : IRequest<CommandResult<GetLoanApplicationGuarantorViewModel>>;