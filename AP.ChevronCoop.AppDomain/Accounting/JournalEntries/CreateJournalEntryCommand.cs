using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.JournalEntries
{
    public partial class CreateJournalEntryCommand : CreateCommand, IRequest<CommandResult<JournalEntryViewModel>>
    {

        public string TransactionEntryNo { get; set; }

        public string TransactionJournalId { get; set; }
        public string TransactionJournalNo { get; set; }
        public string TransactionJournalTitle { get; set; }

       
        public string AccountId { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }

       
        //public TransactionEntryType EntryType { get; set; }
        public string EntryType { get; set; }

        public int DecimalPlaces { get; set; } = 3;

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public DateTime TransactionDate { get; set; }

        public bool IsPosted { get; set; } = false;


        public string PostedByUserName { get; set; }


        public DateTime? DatePosted { get; set; }

        [Required]
        public bool IsReversed { get; set; } = false;


        public string ReversedByUserName { get; set; }


        public DateTime? ReversalDate { get; set; }


        public string Memo { get; set; }

    }







}
