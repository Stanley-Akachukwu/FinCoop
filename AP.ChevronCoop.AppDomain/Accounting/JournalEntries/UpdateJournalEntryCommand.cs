using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.JournalEntries
{
    public partial class UpdateJournalEntryCommand : UpdateCommand, IRequest<CommandResult<JournalEntryViewModel>>
    {

        [MaxLength(64)]
        [Required]
        public string TransactionEntryNo { get; set; }

        [MaxLength(40)]
        [Required]
        public string TransactionJournalId { get; set; }

        [MaxLength(40)]
        [Required]
        public string AccountId { get; set; }

        public string GLCode { get; set; }
        public string GLName { get; set; }

        [MaxLength(32)]
        [Required]
        //public TransactionEntryType EntryType { get; set; }
        public string EntryType { get; set; }

        [Required]
        public int DecimalPlaces { get; set; }

        [Required]
        public decimal Debit { get; set; }

        [Required]
        public decimal Credit { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public bool IsPosted { get; set; }


        public string PostedByUserName { get; set; }


        public DateTime? DatePosted { get; set; }

        [Required]
        public bool IsReversed { get; set; }


        public string ReversedByUserName { get; set; }


        public DateTime? ReversalDate { get; set; }

        [MaxLength(1024)]

        public string Memo { get; set; }

    }







}
