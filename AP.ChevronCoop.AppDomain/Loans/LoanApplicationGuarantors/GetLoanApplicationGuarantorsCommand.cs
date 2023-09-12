using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public record GetLoanApplicationGuarantorsCommand
  (string LoanApplicationId) : IRequest<CommandResult<List<GetLoanApplicationGuarantorsViewModel>>>;