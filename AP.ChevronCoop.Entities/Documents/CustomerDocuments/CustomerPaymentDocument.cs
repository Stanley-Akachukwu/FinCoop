using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;

namespace AP.ChevronCoop.Entities.Documents.CustomerDocuments;

public class CustomerPaymentDocument : BaseEntity<string>
{
    public CustomerPaymentDocument()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    [Required]
    public string CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; }

    public string Document { get; set; }

    public string MimeType { get; set; }
    public string FileName { get; set; }
    public int FileSize { get; set; }
    public MemberPaymentUploadType DocumentType { get; set; }
    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}
