using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Documents.CustomerDocuments;

public class CreateCustomerPaymentDocumentCommand : CreateCommand,
  IRequest<CommandResult<CustomerPaymentDocumentViewModel>>
{
  public string CustomerId { get; set; }
  public string Document { get; set; }
  public MemberPaymentUploadType DocumentType { get; set; }
  public string MimeType { get; set; }
  public string FileName { get; set; }
  public int FileSize { get; set; }
}