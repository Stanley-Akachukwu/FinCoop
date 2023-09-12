using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public class UpdateLoanProductStatusCommand : IRequest<CommandResult<LoanProductViewModel>>
{
  public string Id { get; set; }
  public ProductStatus Status { get; set; }
}