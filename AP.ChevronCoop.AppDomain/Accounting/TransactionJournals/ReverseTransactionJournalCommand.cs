using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.TransactionJournals
{
    public partial class ReverseTransactionJournalCommand : CreateCommand, IRequest<CommandResult<TransactionJournalViewModel>>
    {

       
        public string Id { get; set; }

        [MaxLength(40)]
        [Required] public string TransactionNo { get; set; }

        public string ReversedByUserId { get; set; }
        public string ReversedByUserName { get; set; }

        public DateTime ReversalDate { get; set; }

        public string Memo { get; set; }


    }


}
