using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.CustomerLoanProductPublications;

public class UpdateLoanProductPublicationCommand : UpdateCommand,
  IRequest<CommandResult<LoanProductPublicationViewModel>>
{
  public PublicationType PublicationType { get; set; }

  public string ProductId { get; set; }

  public List<string> EntityIds { get; set; }
}