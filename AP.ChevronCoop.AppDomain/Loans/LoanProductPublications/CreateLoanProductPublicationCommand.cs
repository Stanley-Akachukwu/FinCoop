using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel;

namespace AP.ChevronCoop.AppDomain.Loans.CustomerLoanProductPublications;

public class CreateLoanProductPublicationCommand : CreateCommand,
  IRequest<CommandResult<LoanProductPublicationViewModel>>
{
    public PublicationType PublicationType { get; set; }

    public string ProductId { get; set; }

    [Description("CustomerIds, DepartmentId")]
    public List<string> EntityIds { get; set; }
}