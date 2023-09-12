using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.TransactionJournals
{
    public partial class PostTransactionJournalCommand : CreateCommand, IRequest<CommandResult<TransactionJournalViewModel>>
    {

        
        public string Id { get; set; }

        [MaxLength(40)]
        [Required] 
        public string TransactionNo { get; set; }

        public bool IsPosted { get; set; }

        public string PostedByUserId { get; set; }
        public string PostedByUserName { get; set; }


        public DateTime DatePosted { get; set; }

        public string Memo { get; set; }


    }


}
