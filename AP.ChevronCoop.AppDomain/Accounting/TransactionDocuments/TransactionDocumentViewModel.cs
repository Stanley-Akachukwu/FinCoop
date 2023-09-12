using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.TransactionDocuments
{
    public partial class TransactionDocumentViewModel : BaseViewModel
    {

        [MaxLength(64)]
        [Required]
        public string DocumentNo { get; set; }

        [MaxLength(900)]
        [Required]
        public string TransactionJournalId { get; set; }

        [MaxLength(80)]
        [Required]
        public string DocumentTypeId { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        public byte[] Document { get; set; }

        public string DocumentUrl { get; set; }
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
