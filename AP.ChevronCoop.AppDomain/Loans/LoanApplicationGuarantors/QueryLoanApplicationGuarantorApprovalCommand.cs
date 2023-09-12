using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;

public record QueryLoanApplicationGuarantorApprovalCommand
  (string GuarantorId) : IRequest<CommandResult<List<LoanApplicationGuarantorApprovalViewModel>>>;