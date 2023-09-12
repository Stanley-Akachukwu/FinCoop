using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public record GetUserLoanProductsCommand
  (LoanProductType LoanType, string ApplicationUserId) : IRequest<CommandResult<List<LoanProductViewModel>>>;