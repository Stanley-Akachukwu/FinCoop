using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public class PublishLoanProductCommand : IRequest<CommandResult<LoanProductViewModel>>
{
  public string ProductId { get; set; }
  public string PublishedByUserId { get; set; }
  public PublicationType PublicationType { get; set; }
  public List<string> Targets { get; set; }
}