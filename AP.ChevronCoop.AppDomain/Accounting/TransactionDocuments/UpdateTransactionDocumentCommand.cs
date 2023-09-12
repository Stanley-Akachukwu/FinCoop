using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.TransactionDocuments
{
    public partial class UpdateTransactionDocumentCommand : UpdateCommand, IRequest<CommandResult<TransactionDocumentViewModel>>
    {

        [MaxLength(64)]
        [Required]
        public string DocumentNo { get; set; }

        [MaxLength(40)]
        [Required]
        public string TransactionJournalId { get; set; }

        [MaxLength(40)]
        [Required]
        public string DocumentTypeId { get; set; }

        [MaxLength(128)]
        [Required]
        public string Name { get; set; }


        public byte[] Document { get; set; }


        public string DocumentUrl { get; set; }

        
    }







}
